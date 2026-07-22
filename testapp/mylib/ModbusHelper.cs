using System;
using System.IO.Ports;
using NModbus;
using NModbus.IO;
using NModbus.Serial;

namespace testapp
{
    /// <summary>
    /// 统一的 Modbus CRC16 校验工具类，替代项目中 30+ 处重复的 crc16() 实现。
    /// 使用标准 Modbus 多项式 0xA001。
    /// </summary>
    public static class ModbusCrc16
    {
        /// <summary>
        /// 计算 Modbus CRC16 校验值（多项式 0xA001）
        /// </summary>
        /// <param name="data">需要计算的数据</param>
        /// <returns>CRC16 校验值</returns>
        public static ushort Compute(byte[] data)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        /// <summary>
        /// 给数据追加 CRC16 校验字节（低字节在前，高字节在后）
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns>带 CRC16 校验的数据</returns>
        public static byte[] AppendCrc(byte[] data)
        {
            byte[] result = new byte[data.Length + 2];
            Array.Copy(data, result, data.Length);
            ushort crc = Compute(data);
            result[data.Length] = (byte)(crc & 0xFF);       // 低字节
            result[data.Length + 1] = (byte)((crc >> 8) & 0xFF); // 高字节
            return result;
        }

        /// <summary>
        /// 验证数据（含 CRC16）是否正确
        /// </summary>
        /// <param name="data">含 CRC16 校验的数据</param>
        /// <returns>true 表示校验通过</returns>
        public static bool Validate(byte[] data)
        {
            if (data == null || data.Length < 3)
                return false;

            byte[] payload = new byte[data.Length - 2];
            Array.Copy(data, payload, data.Length - 2);
            ushort computed = Compute(payload);
            ushort received = (ushort)(data[data.Length - 2] | (data[data.Length - 1] << 8));
            return computed == received;
        }
    }

    /// <summary>
    /// 统一的 Modbus 通信助手类，封装 NModbus 库，替代项目中多种 Modbus 实现。
    /// 支持 RTU (串口) 和 TCP 两种模式。
    /// 功能码覆盖：0x01 读线圈、0x02 读离散输入、0x03 读保持寄存器、
    /// 0x04 读输入寄存器、0x05 写单线圈、0x06 写单寄存器、
    /// 0x0F 写多线圈、0x10 写多寄存器。
    /// </summary>
    public static class ModbusHelper
    {
        #region RTU (Serial Port)

        /// <summary>
        /// 创建 Modbus RTU Master（基于串口）
        /// NModbus 3.0.83 使用工厂模式：ModbusFactory.CreateRtuMaster(IStreamResource)
        /// </summary>
        public static IModbusMaster CreateRtuMaster(
            string portName, int baudRate, int dataBits = 8,
            StopBits stopBits = StopBits.One, Parity parity = Parity.None)
        {
            var serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                ReadTimeout = 3000,
                WriteTimeout = 3000,
            };
            serialPort.Open();
            var adapter = new SerialPortAdapter(serialPort);
            var factory = new ModbusFactory();
            return factory.CreateRtuMaster(adapter);
        }

        /// <summary>
        /// 从已打开的 SerialPort 创建 Modbus RTU Master
        /// </summary>
        public static IModbusMaster CreateRtuMaster(SerialPort serialPort)
        {
            var adapter = new SerialPortAdapter(serialPort);
            var factory = new ModbusFactory();
            return factory.CreateRtuMaster(adapter);
        }

        #endregion

        #region TCP

        /// <summary>
        /// 创建 Modbus TCP Master
        /// NModbus 3.0.83 使用工厂模式：ModbusFactory.CreateIpMaster(IStreamResource)
        /// </summary>
        public static IModbusMaster CreateTcpMaster(string ipAddress, int port = 502)
        {
            var tcpClient = new System.Net.Sockets.TcpClient(ipAddress, port);
            var adapter = new TcpClientAdapter(tcpClient);
            var factory = new ModbusFactory();
            return factory.CreateIpMaster(adapter);
        }

        #endregion

        #region Read Operations

        /// <summary>读线圈 (功能码 0x01)</summary>
        public static bool[] ReadCoils(IModbusMaster master, byte slaveId, ushort startAddress, ushort count)
            => master.ReadCoils(slaveId, startAddress, count);

        /// <summary>读离散输入 (功能码 0x02)</summary>
        public static bool[] ReadInputs(IModbusMaster master, byte slaveId, ushort startAddress, ushort count)
            => master.ReadInputs(slaveId, startAddress, count);

        /// <summary>读保持寄存器 (功能码 0x03)</summary>
        public static ushort[] ReadHoldingRegisters(IModbusMaster master, byte slaveId, ushort startAddress, ushort count)
            => master.ReadHoldingRegisters(slaveId, startAddress, count);

        /// <summary>读输入寄存器 (功能码 0x04)</summary>
        public static ushort[] ReadInputRegisters(IModbusMaster master, byte slaveId, ushort startAddress, ushort count)
            => master.ReadInputRegisters(slaveId, startAddress, count);

        /// <summary>读单个保持寄存器 (便捷方法)</summary>
        public static ushort ReadHoldingRegister(IModbusMaster master, byte slaveId, ushort address)
            => master.ReadHoldingRegisters(slaveId, address, 1)[0];

        #endregion

        #region Write Operations

        /// <summary>写单个线圈 (功能码 0x05)</summary>
        public static void WriteSingleCoil(IModbusMaster master, byte slaveId, ushort address, bool value)
            => master.WriteSingleCoil(slaveId, address, value);

        /// <summary>写单个寄存器 (功能码 0x06)</summary>
        public static void WriteSingleRegister(IModbusMaster master, byte slaveId, ushort address, ushort value)
            => master.WriteSingleRegister(slaveId, address, value);

        /// <summary>写多个线圈 (功能码 0x0F)</summary>
        public static void WriteMultipleCoils(IModbusMaster master, byte slaveId, ushort startAddress, bool[] values)
            => master.WriteMultipleCoils(slaveId, startAddress, values);

        /// <summary>写多个寄存器 (功能码 0x10)</summary>
        public static void WriteMultipleRegisters(IModbusMaster master, byte slaveId, ushort startAddress, ushort[] values)
            => master.WriteMultipleRegisters(slaveId, startAddress, values);

        #endregion

        #region Raw Frame (for backward compatibility with tan_modbus pattern)

        /// <summary>
        /// 构建带 CRC16 的 Modbus RTU 帧（替代手动 tan_modbus 函数）
        /// </summary>
        /// <param name="slaveAddress">从站地址</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="data">功能码后的数据</param>
        /// <returns>完整的 Modbus RTU 帧（含 CRC16）</returns>
        public static byte[] BuildRtuFrame(byte slaveAddress, byte functionCode, byte[] data)
        {
            byte[] frame = new byte[2 + data.Length];
            frame[0] = slaveAddress;
            frame[1] = functionCode;
            Array.Copy(data, 0, frame, 2, data.Length);
            return ModbusCrc16.AppendCrc(frame);
        }

        /// <summary>
        /// 构建读保持寄存器的 Modbus RTU 请求帧
        /// </summary>
        public static byte[] BuildReadHoldingRegistersFrame(byte slaveAddress, ushort startAddress, ushort count)
        {
            byte[] data = new byte[4];
            data[0] = (byte)(startAddress >> 8);
            data[1] = (byte)(startAddress & 0xFF);
            data[2] = (byte)(count >> 8);
            data[3] = (byte)(count & 0xFF);
            return BuildRtuFrame(slaveAddress, 0x03, data);
        }

        /// <summary>
        /// 构建写单个寄存器的 Modbus RTU 请求帧
        /// </summary>
        public static byte[] BuildWriteSingleRegisterFrame(byte slaveAddress, ushort address, ushort value)
        {
            byte[] data = new byte[4];
            data[0] = (byte)(address >> 8);
            data[1] = (byte)(address & 0xFF);
            data[2] = (byte)(value >> 8);
            data[3] = (byte)(value & 0xFF);
            return BuildRtuFrame(slaveAddress, 0x06, data);
        }

        /// <summary>
        /// 解析 Modbus RTU 响应中的寄存器数据
        /// </summary>
        public static ushort[] ParseRegisterResponse(byte[] response)
        {
            if (response == null || response.Length < 5)
                return Array.Empty<ushort>();

            // 响应格式: [slaveId][funcCode][byteCount][data...][CRC_lo][CRC_hi]
            int byteCount = response[2];
            ushort[] registers = new ushort[byteCount / 2];
            for (int i = 0; i < registers.Length; i++)
            {
                int offset = 3 + i * 2;
                registers[i] = (ushort)((response[offset] << 8) | response[offset + 1]);
            }
            return registers;
        }

        #endregion
    }
}
