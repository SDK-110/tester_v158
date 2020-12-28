using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{



    public class ch341_iic_spi
    {
       



        //FLASH 配置状态寄存器地址
        public static byte WRITE_ENABLE = 0X06; //写使能
        public static byte WRITE_DISABLE = 0X04;    //写禁止
        public static byte READ_STATUS_REG1 = 0X05; //读状态寄存器
        public static byte READ_STATUS_REG2 = 0X35; //读状态寄存器
        public static byte READ_DATA = 0X03;    //读字节
        public static byte EWSR = 0X50;    //使能写状态寄存器
        public static byte FAST_READ = 0X0B;    //快读指令
        public static byte PAGE_PROGRAM = 0X02; //Byte Prog Mode
        public static byte SECTOR_ERASE_4K = 0X20;  //Erase 4 KByte of memory array
        public static byte BLOCK_ERASE_32K = 0X52; //Erase 32 KByte block of memory array
        public static byte BLOCK_ERASE_64K = 0XD8;  //Erase 64 KByte block of memory array
        public static byte CHIP_ERASE = 0XC7;   //擦除整个FLASH芯片

        public static void memset(byte[] buf, byte val, int size)
        {
            int i;
            for (i = 0; i < size; i++)
                buf[i] = val;
        }

        public static void memcpy(byte[] dst, int dst_offst, byte[] src, int src_offst, uint len)
        {
            for (int i = 0; i < len; i++)
            {
                dst[dst_offst++] = src[src_offst++];
            }

        }

        public static bool ReadI2C(uint mIndex, byte SlaveAddr, byte DataAddr, byte readData)    //连读模式
        {
            UInt32 writeLen = 0, readLen = 1;
            byte[] writeData = new byte[4], rData = new byte[1];
            memset(writeData, 0xFF, 4);
            writeData[0] = SlaveAddr;
            writeData[1] = DataAddr;
            writeLen = 2;
            bool bRet = USBIOXdll.USBIO_StreamI2C(mIndex, writeLen, writeData, readLen, rData);
            readData = rData[0];
            return bRet;
        }
        public static bool ReadI2C(uint mIndex, byte SlaveAddr, byte DataAddr, byte[] readData, UInt32 readLen)    //连读模式
        {
            UInt32 writeLen = 0;
            byte[] writeData = new byte[4];
            memset(writeData, 0xFF, 4);
            writeData[0] = SlaveAddr;
            writeData[1] = DataAddr;
            writeLen = 2;
            bool bRet = USBIOXdll.USBIO_StreamI2C(mIndex, writeLen, writeData, readLen, readData);
            return bRet;
        }

        public static bool WriteI2C(uint mIndex, byte SlaveAddr, byte DataAddr, byte data)     //单写模式
        {
            UInt32 writeLen = 3;
            UInt32 readLen = 0;
            byte[] writeData = new byte[4];
            byte[] readData = new byte[256];
            memset(writeData, 0xFF, 4);
            memset(readData, 0x00, 256);
            writeData[0] = SlaveAddr;
            writeData[1] = DataAddr;
            writeData[2] = data;
            bool bRet = USBIOXdll.USBIO_StreamI2C(mIndex, writeLen, writeData, readLen, readData);
            return bRet;
        }

        public static bool WriteI2C(uint mIndex, byte SlaveAddr, byte DataAddr, byte[] data, UInt32 len)     //多写模式
        {
            UInt32 writeLen = len + 2;
            UInt32 readLen = 0;
            byte[] writeData = new byte[writeLen];
            byte[] readData = new byte[256];
            //memset(writeData, 0xFF, 4);
            memset(readData, 0x00, 256);

            writeData[0] = SlaveAddr;
            writeData[1] = DataAddr;
            memcpy(writeData, 2, data, 0, len);
            bool bRet = USBIOXdll.USBIO_StreamI2C(mIndex, writeLen, writeData, readLen, readData);
            return bRet;
        }
        /// <summary>
        /// SPI擦除flash操作
        /// </summary>
        /// <param name="mIndex">指定CH341设备序号</param>
        /// <param name="m_iChipSelect">片选控制, 位7为0则忽略片选控制, 位7为1则参数有效: 位1位0为00/01/10分别选择D0/D1/D2引脚作为低电平有效片选</param>
        /// <param name="Erase_Select">擦除类型</param>
        /// <param name="sAdd">地址</param>
        public static void SPIErase(uint mIndex, uint m_iChipSelect, byte Erase_Select, uint sAdd)
        {
            byte[] data = new byte[4];
            USBIOXdll.USBIO_SetStream(mIndex, 0x80);
            memset(data, 0xFF, 4);
            data[0] = WRITE_ENABLE;
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, data);

            data[0] = Erase_Select;
            data[1] = (byte)((sAdd & 0xFF0000) >> 16);
            data[2] = (byte)((sAdd & 0x00FF00) >> 8);
            data[3] = (byte)(sAdd & 0x0000FF);
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 4, data);

            data[0] = WRITE_DISABLE;
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, data);

        }
        /// <summary>
        /// SPI写入flash数据
        /// </summary>
        /// <param name="mIndex">设备号</param>
        /// <param name="m_iChipSelect">片选</param>
        /// <param name="sAdd">起始地址</param>
        /// <param name="WriteData">数据</param>
        public static void WriteSPI(uint mIndex, uint m_iChipSelect, uint sAdd, byte[] WriteData)
        {
            byte[] status = new byte[1];
            byte[] data = new byte[260];
            int len = WriteData.Length;
            int nPage = 0;
            USBIOXdll.USBIO_SetStream(mIndex, 0x80);
            while (len > 256)
            {

                status[0] = WRITE_ENABLE;
                USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, status);
                data[0] = PAGE_PROGRAM;
                data[1] = (byte)((sAdd & 0xFF0000) >> 16);
                data[2] = (byte)((sAdd & 0x00FF00) >> 8);
                data[3] = (byte)(sAdd & 0x0000FF);
                memcpy(data, 4, WriteData, nPage * 256, 256);
                USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 260, data);
                status[0] = WRITE_DISABLE;
                USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, status);
                USBIOXdll.USBIO_SetDelaymS(mIndex, 2);
                len -= 256;
                nPage++;
                sAdd += 256;
            }

            status[0] = WRITE_ENABLE;
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, status);
            data[0] = PAGE_PROGRAM;
            data[1] = (byte)((sAdd & 0xFF0000) >> 16);
            data[2] = (byte)((sAdd & 0x00FF00) >> 8);
            data[3] = (byte)(sAdd & 0x0000FF);
            memcpy(data, 4, WriteData, nPage * 256, (uint)len);
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, (uint)(len + 4), data);
            status[0] = WRITE_DISABLE;
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 1, status);
        }
        /// <summary>
        /// SPI读取flash数据
        /// </summary>
        /// <param name="mIndex">设备号</param>
        /// <param name="m_iChipSelect">片选</param>
        /// <param name="sAdd">起始地址</param>
        /// <param name="ReadData">读取数据返回数组</param>
        /// <param name="ReadLen">读取长度</param>
        public static void ReadSPI(uint mIndex, uint m_iChipSelect, uint sAdd, byte[] ReadData, uint ReadLen)
        {
            byte[] status = new byte[2564];
            int i = 0;

            USBIOXdll.USBIO_SetStream(mIndex, 0x80);
            //USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 4, status);
            while (ReadLen > 2560)
            {
                status[0] = READ_DATA;
                status[1] = (byte)((sAdd & 0xFF0000) >> 16);
                status[2] = (byte)((sAdd & 0x00FF00) >> 8);
                status[3] = (byte)(sAdd & 0x0000FF);
                USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, 2564, status);
                memcpy(ReadData, i * 2560, status, 4, 2560);
                ReadLen -= 2560;
                i++;
                sAdd += 2560;
            }
            status[0] = READ_DATA;
            status[1] = (byte)((sAdd & 0xFF0000) >> 16);
            status[2] = (byte)((sAdd & 0x00FF00) >> 8);
            status[3] = (byte)(sAdd & 0x0000FF);
            USBIOXdll.USBIO_StreamSPI4(mIndex, m_iChipSelect, ReadLen + 4, status);
            memcpy(ReadData, i * 2560, status, 4, ReadLen);
        }


    }


}