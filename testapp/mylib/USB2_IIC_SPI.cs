using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace testapp
{
    enum SPItranMode { mode00 = 0, mode01 = 1, mode10 = 2, mode11 = 3 }
    enum SPImasterSlave { SPImaster = 0, SPISlave = 1 }
    enum SPIfreq { f100k = 1, f200k = 2, f400k = 3, f600k = 4, f1M = 5, f2M = 6, f4M = 7, f8M = 8, f12M = 9, f24M = 10 }
    enum IICfreq { f100k = 10, f200k = 20, f400k = 40, f600k = 60, f8OOK = 80, f1M = 100 }

 
    class USB2_IIC_SPI : SerialPort
    {
        volatile byte[] SPIMasterCommadHead = { 0x02, 0x01, 0x55, 0x58, 0xff, 0xff, 0xff, 0xff, 0x16 };
        volatile byte[] SPISlaveCommadHead  =  { 0x02, 0x04, 0x55, 0x5b, 0xff, 0xff, 0x00, 0xff, 0x16 };
        string revbuf = "";


        public event debuginfosend debugsendstr;

        

        public debuginfosend setdebuginfosend
        {

            set { debugsendstr = value; }
        }

        void debugstrsend(string m)
        {

            if (debugsendstr != null) debugsendstr(m);

        }

        #region /*--------------message loop dll upload-------------*/

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(
            IntPtr hWnd, // handle to destination window 
            uint Msg, // message 
            uint wParam, // first message parameter 
            uint lParam // second message parameter 
            );

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public const int USER = 0x0400;
        public const int WM_SENDA = USER + 101;
        public const int WM_SENDB = USER + 102;
        public const int WM_SENDC = USER + 103;
        public const int WM_SENDD = USER + 104;
        public const int WM_SENDE = USER + 105;
        public const int WM_SHOWNUM = USER + 106;
        public const int WM_FASTID = USER + 107;
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;

        callbackfuc forsendwinmessag;

        public callbackfuc setinterfacefuc
        {

            set { forsendwinmessag = value; }
        }

   
        #endregion

        public USB2_IIC_SPI(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.ReadTimeout = 15000;
          //   base.DataReceived += __DataReceived;
            if (base.IsOpen == false)
            {
                base.Open();

            }
            base.PinChanged += Comline_PinChanged;

        }

        private void __DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
        }

        private void Comline_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            return;
            if (forsendwinmessag != null)
            {
                forsendwinmessag();
            }
        }
        bool  SendSPIData() {

            if (this.IsOpen)
            {


            }
            else { 
            
            
            }

            return false;
        }

        public void GetSPIconfig(int datalen=8,SPIfreq sPIfreq=SPIfreq.f2M,SPItranMode mode=SPItranMode.mode00) {

            SPIMasterCommadHead[0] = 0x02; SPIMasterCommadHead[1] = 0x01; SPIMasterCommadHead[2] = 0x55; SPIMasterCommadHead[3] = 0x58;
            SPIMasterCommadHead[4] = 0xFF;SPIMasterCommadHead[5] = 0xFF;SPIMasterCommadHead[6] = 0xFF;SPIMasterCommadHead[7] = 0xFF;
            SPIMasterCommadHead[8] = 0x16; 
            SPISlaveCommadHead[0] = 0x02;SPISlaveCommadHead[1] = 0x04;SPISlaveCommadHead[2] = 0x55; SPISlaveCommadHead[3] = 0x5B;
            SPISlaveCommadHead[4] = 0xFF;SPISlaveCommadHead[5] = 0xFF;SPISlaveCommadHead[6] = 0x00;SPISlaveCommadHead[7] = 0xFF;
            SPISlaveCommadHead[8] = 0x16;
            /*数据长度*/
            SPIMasterCommadHead[5] = (byte)(datalen-1);
            SPISlaveCommadHead[5] = (byte)(datalen - 1);
            /*"频率设置不在范围"*/
            SPIMasterCommadHead[6] = (byte)sPIfreq;
            SPISlaveCommadHead[6] = (byte)sPIfreq;
            SPIMasterCommadHead[7] = (byte)mode;
            SPISlaveCommadHead[7] = (byte)mode;


        }

        public int SPISendHEX(byte [] senddata,
                               int MasterSlave_sel=1,/*0=主,1=从*/
                                int datalen = 8,
                                SPIfreq sPIfreq = SPIfreq.f2M,
                                 SPItranMode mode = SPItranMode.mode00
                                 ) {
            if (this.IsOpen == false) this.Open();
            if (senddata.Length % 2 != 0) return 0;

            GetSPIconfig(8, SPIfreq.f2M, SPItranMode.mode00);
            try
            {
                if (MasterSlave_sel == 0)
                {

                    SPIMasterCommadHead[4] = (byte)senddata.Length;
                    this.Write(SPIMasterCommadHead, 0, 9);
                    this.Write(senddata, 0, senddata.Length);
                }
                else
                {
                    SPISlaveCommadHead[4] = (byte)senddata.Length;
                    this.Write(SPISlaveCommadHead, 0, 9);
                    this.Write(senddata, 0, senddata.Length);


                }

                return 1;
            }
            catch {


                return -1;
            }

       




        }

        public void I2CSendHEX(
                                byte localAddr, /* 7位本机地址最大值只能0x3F */
                                byte remoteAddr, 
                                byte[] senddata,
                                int MasterSlave_send_rec_select=0,/*0=主发送,1=从发送*/
                                IICfreq iICfreq = IICfreq.f1M/**/
                              )
        {


            byte[] I2CMasterSendHead = { 0x01, 0x01, 0x55, 0x57, 0xFF, 0xFF, 0xFF, 0xFF, 0x16 };
            byte[] I2CSlaveSendHead = { 0x01, 0x03, 0x55, 0x59, 0xFF, 0xFF, 0xFF, 0xFF, 0x16 };




            // 首先获取一些参数
            byte AT24CxxSel = new byte();
            UInt16 toGetI2CRecvLen = new UInt16();  // 这个txb长度为3位，有可能超过256！
            byte[] I2CAT24CxxReadAddr = { 0x00 };
          //  byte[] LPC1114ResetCommand = { 0x06, 0x01, 0x55, 0x5C, 0x00, 0x00, 0x00, 0x00, 0x16 };   // 复位指令
            if (this.IsOpen == false) this.Open();
         //   this.Write(LPC1114ResetCommand, 0, 9);
            try { 
                switch (MasterSlave_send_rec_select)  // 不同模式下有不同的发送功能
                {
                    case 0:
                        I2CMasterSendHead[4] = (byte)senddata.Length;  // 这个长度是，要发送的数据长度！
                        I2CMasterSendHead[5] = remoteAddr;  // 16进制的ADDR
                        I2CMasterSendHead[6] = (byte)iICfreq;   // 频率
                        this.DiscardInBuffer();
                        this.Write(I2CMasterSendHead, 0, 9);//发送数据
                        this.Write(senddata, 0, senddata.Length);//发送数据
                    

                        break;
 
                    case 1:
                        I2CSlaveSendHead[4] = (byte)senddata.Length;
                        I2CSlaveSendHead[6] = (byte)iICfreq;
                        I2CSlaveSendHead[7] = localAddr;
                        this.DiscardInBuffer();
                        this.Write(I2CSlaveSendHead, 0, 9);
                        this.Write(senddata, 0, senddata.Length);
                 
                        break;
                    default: 
                    break;
                }
            }
            catch
            {
              
                return;
            }
        }
        public byte[] I2CRecHEX(
                                byte localAddr, /* 7位本机地址最大值只能0x3F */
                                byte remoteAddr,
                                 int IICMasterSlave_Send_Rec_sel = 0,/*0=主接收,2从接收*/
                                IICfreq iICfreq = IICfreq.f1M,/**/
                                int recvlen = 1, /*不要大于200*/
                                int at24readdress = 0
                              
                              )
        {


 
            byte[] I2CMasterRecvHead = { 0x01, 0x02, 0x55, 0x58, 0xFF, 0xFF, 0xFF, 0xFF, 0x16 };

            byte[] I2CSlaveRecvHead = { 0x01, 0x04, 0x55, 0x5A, 0xFF, 0xFF, 0xFF, 0xFF, 0x16 };



            // 首先获取一些参数
            byte AT24CxxSel = new byte();
            UInt16 toGetI2CRecvLen = new UInt16();  // 这个txb长度为3位，有可能超过256！
            byte[] I2CAT24CxxReadAddr = { (byte)at24readdress };
            //  byte[] LPC1114ResetCommand = { 0x06, 0x01, 0x55, 0x5C, 0x00, 0x00, 0x00, 0x00, 0x16 };   // 复位指令
            if (this.IsOpen == false) this.Open();
            //   this.Write(LPC1114ResetCommand, 0, 9);
            try
            {
                if (IICMasterSlave_Send_Rec_sel == 0) { // 不同模式下有不同的发送功能


                    I2CMasterRecvHead[4] = (byte)recvlen;  // 只会多发一个数据
                    I2CMasterRecvHead[5] = remoteAddr;
                    I2CMasterRecvHead[6] = (byte)iICfreq;
                    I2CMasterRecvHead[7] = (byte)0;
                    this.Write(I2CMasterRecvHead, 0, 9);//发送数据
                    this.Write(I2CAT24CxxReadAddr, 0, 1);  // 发送接收长度的数据
                    System.Threading.Thread.Sleep(1000);
                    byte[] ret = new byte[this.BytesToRead];
                    this.Read(ret, 0, this.BytesToRead);
                    return ret;
                }

                if (IICMasterSlave_Send_Rec_sel == 0)
                {
                    I2CSlaveRecvHead[6] = (byte)iICfreq;
                    I2CSlaveRecvHead[7] = localAddr;
                    this.Write(I2CSlaveRecvHead, 0, 9);
                    System.Threading.Thread.Sleep(1000);
                    byte[] ret = new byte[this.BytesToRead];
                    this.Read(ret, 0, this.BytesToRead);
                    return ret;
                }
            
                }
            catch
            {

                return new byte[] { 0x00};
            }

                return new byte[] { 0x00 };
        }








        ~USB2_IIC_SPI() {

            this.Close();
        
        }
    }
}
