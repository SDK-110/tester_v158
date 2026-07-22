using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

using System.Threading;
using System.ComponentModel;

namespace SLCANWithEvents
{
    /// <summary>
    /// 自定义串口类，继承自 SerialPort
    /// </summary>
    public class SLCANSerialPort : SerialPort
    {
        // 定义数据接收事件

        object lock_obj = new object();
        int glob_canId=-1;
        byte[] data_rev = null;
        volatile int rev_flog=0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="portName">串口号</param>
        /// <param name="baudRate">波特率</param>
        public SLCANSerialPort(string portName, int baudRate=5000000) : base(portName, baudRate)
        {

        
            this.DataReceived += SLCANSerialPort_DataReceived; // 绑定数据接收事件
            this.NewLine="\r";
            this.Open();
            Task.Factory.StartNew(() =>
            {

                ConfigureSLCAN();
            });
           
        }



        /// <summary>
        /// 数据接收事件处理程序
        /// </summary>
        private void SLCANSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                SerialPort sp = (SerialPort)sender;
             

                string data = sp.ReadLine();
                if (data.Length>6)ParseCANFrame(data);   // 触发数据接收事件
                
            }
            catch (TimeoutException)
            {


                testapp.mylib.utility_func.callbackdebuginfo($"超时异常"); 
            }
        }

    

        /// <summary>
        /// 发送 CAN 帧
        /// </summary>
        /// <param name="canId">CAN 标识符</param>
        /// <param name="data">数据字节数组</param>
        public void SendCANFrame(int canId, byte[] data)
        {
            try
            {
                
                if (!this.IsOpen) { 
                
                this.Open ();
                  ConfigureSLCAN();
                }
                data_rev = null;
                canId = -1;
               data = null;
               rev_flog = 0;
                StringBuilder frame = new StringBuilder();
                frame.Append('t'); // 标准帧
                frame.Append(canId.ToString("X3")); // CAN ID（3 字符十六进制）
                frame.Append(data.Length.ToString("X1")); // 数据长度（1 字符十六进制）
                foreach (byte b in data)
                {
                    frame.Append(b.ToString("X2")); // 数据字节（2 字符十六进制）
                }
                Write(frame.ToString()+"\r");
                testapp.mylib.utility_func.callbackdebuginfo($"发送 CAN 帧: {frame}");
            }
            catch { 
            
            
            }


        }

        public void SendCANFrame(int canId, string data_str, int mode=1)
        {
            try
            {

                if (!this.IsOpen)
                {

                    this.Open();
                    ConfigureSLCAN();
                }

                glob_canId = -1;
                data_rev = null;
                rev_flog = 0;
                StringBuilder frame = new StringBuilder();
                frame.Append('t'); // 标准帧
                frame.Append(canId.ToString("X3")); // CAN ID（3 字符十六进制）
                byte[] data = null;
                if (mode == 1)
                {
                    data = Encoding.ASCII.GetBytes(data_str);
                }
                else {

                  data =   HexStringToByteArray(data_str);
                }
               
                frame.Append(data.Length.ToString("X1")); // 数据长度（1 字符十六进制）
                foreach (byte b in data)
                {
                    frame.Append(b.ToString("X2")); // 数据字节（2 字符十六进制）
                }


                Write(frame.ToString()+"\r");
                testapp.mylib.utility_func.callbackdebuginfo($"发送 CAN 帧: {frame}");
            }
            catch
            {


            }


        }

        public  byte[] HexStringToByteArray(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return new byte[0];

            // 移除所有空白字符（包括空格、换行等）
            hex = hex.Replace(" ", string.Empty);

            // 确保是偶数长度
            if (hex.Length % 2 != 0)
                throw new FormatException("十六进制字符串长度必须为偶数");

            // 转换每个字符对为一个字节
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                string currentPair = hex.Substring(i, 2);
                bytes[i / 2] = Convert.ToByte(currentPair, 16);
            }

            return bytes;
        }



        /// <summary>
        /// 配置 SLCAN
        /// </summary>
        /// <param name="slcanPort">SLCAN 串口实例</param>
        private void ConfigureSLCAN(string br = "S4")
        {
            // 设置仲裁相位波特率为 500 kbps
            this.Write(br + "\r");
            Thread.Sleep(100);

            // 启用 CAN FD 模式（如果支持）
           // this.WriteLine("F1");
           // Thread.Sleep(100);

            // 打开 CAN 接口
            this.Write("O" + "\r");
            Thread.Sleep(100);
            

        }

        public string get_data_hex_str(out int status,out int id) {

            for (int i = 0; i < 10; i++) {

                if (rev_flog == 1)
                {
                    status = 1;
                    id = glob_canId;
                    return BitConverter.ToString(data_rev).Replace("-","");
                }
            
            }

            status = -1;
            id = -1;
            return "NA";
        
        
        }

        ~SLCANSerialPort() { 
        
        if(this.IsOpen) this.Close();
        
        }

        public void dispose() {


            try {

                if (this.IsOpen) this.Close();
                this.dispose();
            }

            catch { }

          






        }

        /// <summary>
        /// 解析 CAN 帧
        /// </summary>
        /// <param name="frame">接收到的 CAN 帧字符串</param>
        private  void ParseCANFrame(string frame)
        {
            if (frame.Length < 6) return; // 最小帧长度检查

            char frameType = frame[0];
            switch (frameType)
            {
                case 't': // 标准帧
                case 'T': // 扩展帧
                    string idStr = frame.Substring(1, frameType == 't' ? 3 : 8); // 提取 CAN ID
                    string lenStr = frame.Substring(frameType == 't' ? 4 : 9, 1); // 提取数据长度
                    string dataStr = frame.Substring(frameType == 't' ? 5 : 10);  // 提取数据字段

                    int canId = Convert.ToInt32(idStr, 16);
                    int dataLength = Convert.ToInt32(lenStr, 16);
                    byte[] data = new byte[dataLength];
                    data_rev = new byte[data.Length];
                    for (int i = 0; i < dataLength; i++)
                    {
                        data_rev[i] = data[i] = Convert.ToByte(dataStr.Substring(i * 2, 2), 16);
                        rev_flog = 1;
                        
                    }
                   

                    testapp.mylib.utility_func.callbackdebuginfo($"解析结果 - ID: 0x{idStr}, 数据长度: {dataLength}, 数据: {BitConverter.ToString(data)}");
                    break;

                default:
                    testapp.mylib.utility_func.callbackdebuginfo("未知帧类型。");
                    break;
            }
        }
    }
}