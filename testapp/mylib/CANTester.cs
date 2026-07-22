using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    class CANTester
    {
     
        byte m_connect = 0;
        ComProc mCan = new ComProc();

        public CANTester(int baudrate)
        {
            INIT_CONFIG init_config = new INIT_CONFIG();

            init_config.AccCode = 0;
            init_config.AccMask = 0xffffff;
            init_config.Filter = 0;
            switch (baudrate)
            {
                case 1000: //1000

                    init_config.Timing0 = 0;
                    init_config.Timing1 = 0x14;
                    break;
                case 800: //800

                    init_config.Timing0 = 0;
                    init_config.Timing1 = 0x16;
                    break;
                case 666: //666

                    init_config.Timing0 = 0x80;
                    init_config.Timing1 = 0xb6;
                    break;
                case 500: //500

                    init_config.Timing0 = 0;
                    init_config.Timing1 = 0x1c;
                    break;
                case 400://400

                    init_config.Timing0 = 0x80;
                    init_config.Timing1 = 0xfa;
                    break;
                case 250://250

                    init_config.Timing0 = 0x01;
                    init_config.Timing1 = 0x1c;
                    break;
                case 200://200

                    init_config.Timing0 = 0x81;
                    init_config.Timing1 = 0xfa;
                    break;
                case 125://125

                    init_config.Timing0 = 0x03;
                    init_config.Timing1 = 0x1c;
                    break;
                case 100://100

                    init_config.Timing0 = 0x04;
                    init_config.Timing1 = 0x1c;
                    break;
                case 80://80

                    init_config.Timing0 = 0x83;
                    init_config.Timing1 = 0xff;
                    break;
                case 50://50

                    init_config.Timing0 = 0x09;
                    init_config.Timing1 = 0x1c;
                    break;
                case 126984://126984

                    init_config.Timing0 = 0x02;
                    init_config.Timing1 = 0xcf;
                    break;

            }
            init_config.Mode = 0;
            if (ECANDLL.OpenDevice(1, 0, 0) != ECANStatus.STATUS_OK)
            {

                System.Windows.Forms.MessageBox.Show("Open device fault!");

                return;
            }
            if (ECANDLL.InitCAN(1, 0, 0, ref init_config) != ECANStatus.STATUS_OK)
            {

                System.Windows.Forms.MessageBox.Show("Init can fault!", "Error!");

                ECANDLL.CloseDevice(1, 0);
                return;
            }

            m_connect = 1;
        }
        public string  ResetCan() {
           
            if (ECANDLL.ResetCAN(1, 0, 0) == ECANStatus.STATUS_OK)
            {
                
                return "Reset Success";
            }
            else
            {
                return "Reset Fault";
            }

            
        }

        public void Write(uint id, byte [] data,int Extendedflag = 0,int Remoteflag = 0) {

            CAN_OBJ frameinfo;

            frameinfo = new CAN_OBJ();
            frameinfo.SendType = 0;

            frameinfo.data = new byte[8];
            frameinfo.Reserved = new byte[2];
            frameinfo.ID = id;
            frameinfo.DataLen = (byte)data.Length;
            frameinfo.ExternFlag = (byte)Extendedflag;
            frameinfo.RemoteFlag = (byte)Remoteflag;
            for (int i = 0; i < data.Length; i++) {

                frameinfo.data[i] = data[i];

            }

            this.mCan.gSendMsgBuf[this.mCan.gSendMsgBufHead].ID = frameinfo.ID;
            this.mCan.gSendMsgBuf[this.mCan.gSendMsgBufHead].DataLen = frameinfo.DataLen;
            this.mCan.gSendMsgBuf[this.mCan.gSendMsgBufHead].data = frameinfo.data;
            this.mCan.gSendMsgBuf[this.mCan.gSendMsgBufHead].ExternFlag = frameinfo.ExternFlag;
            this.mCan.gSendMsgBuf[this.mCan.gSendMsgBufHead].RemoteFlag = frameinfo.RemoteFlag;
            this.mCan.gSendMsgBufHead += 1;
            if (this.mCan.gSendMsgBufHead >= ComProc.SEND_MSG_BUF_MAX)
            {
                this.mCan.gSendMsgBufHead = 0;
            }

        }

        public string readError1() {
            CAN_ERR_INFO mErrInfo = new CAN_ERR_INFO();
            if (ECANDLL.ReadErrInfo(1, 0, 0, out mErrInfo) == ECANStatus.STATUS_OK)
            {
               return  string.Format("{0:X4}h", mErrInfo.ErrCode) + "," +
                       string.Format("{0:X4}h", mErrInfo.Passive_ErrData[1])+ "," +
                       string.Format("{0:X4}h", mErrInfo.Passive_ErrData[2]);

            }
            else
            {

               return "Read Error Fault";
            }


        }

        public string ReadError2()
        {

            CAN_ERR_INFO mErrInfo = new CAN_ERR_INFO();

            if (ECANDLL.ReadErrInfo(1, 0, 1, out mErrInfo) == ECANStatus.STATUS_OK)
            {
               return string.Format("{0:X4}h", mErrInfo.ErrCode);

            }
            else
            {

                return   "Read Error Fault";
            }



        }

        public string ReadMessages() {
            string rst="";
            CAN_OBJ frameinfo = new CAN_OBJ();
            int mCount = 0;
            while (this.mCan.gRecMsgBufHead != this.mCan.gRecMsgBufTail)
            {
                string tmpstr;
                frameinfo = this.mCan.gRecMsgBuf[this.mCan.gRecMsgBufTail];
                this.mCan.gRecMsgBufTail += 1;
                if (this.mCan.gRecMsgBufTail >= ComProc.REC_MSG_BUF_MAX)
                {
                    this.mCan.gRecMsgBufTail = 0;
                }
                string str = "Rec: ";
                if (frameinfo.TimeFlag == 0)
                {
                    tmpstr = "Time:  ";
                }
                else
                {
                    tmpstr = "Time:" + string.Format("{0:X8}h", frameinfo.TimeStamp);
                }
                str = str + tmpstr;
                tmpstr = "  ID:" + string.Format("{0:X8}h", frameinfo.ID);
                str = str + tmpstr + " Format:";
                if (frameinfo.RemoteFlag == 0)
                {
                    tmpstr = "Data ";
                }
                else
                {
                    tmpstr = "Romte ";
                }
                str = str + tmpstr + " Type:";
                if (frameinfo.ExternFlag == 0)
                {
                    tmpstr = "Stand ";
                }
                else
                {
                    tmpstr = "Exten ";
                }
                str = str + tmpstr;
                if (frameinfo.RemoteFlag == 0)
                {
                    str = str + " Data:";
                    if (frameinfo.DataLen > 8)
                    {
                        frameinfo.DataLen = 8;
                    }
                    int mlen = frameinfo.DataLen - 1;
                    for (int j = 0; j <= mlen; j++)
                    {
                        tmpstr = string.Format("{0:X2}h", frameinfo.data[j]);
                        str = str + tmpstr;
                    }
                }
                rst += (rst == "") ? "": "," + str;
            
                mCount++;
                if (mCount >= 50)
                {
                    break;
                }
             
            }


            return rst;

        }

        public byte[][]  ReadMessages(ref string time,ref uint id, ref string RemoteFlag,ref string ExternFlag)
        {
            string rst = "";
            byte[][] temp = new byte[50][] ;
            CAN_OBJ frameinfo = new CAN_OBJ();
            int mCount = 0;
            while (this.mCan.gRecMsgBufHead != this.mCan.gRecMsgBufTail)
            {
                string tmpstr;
                frameinfo = this.mCan.gRecMsgBuf[this.mCan.gRecMsgBufTail];
                this.mCan.gRecMsgBufTail += 1;
                if (this.mCan.gRecMsgBufTail >= ComProc.REC_MSG_BUF_MAX)
                {
                    this.mCan.gRecMsgBufTail = 0;
                }
             
                if (frameinfo.TimeFlag == 0)
                {
                    time = "-1";
                }
                else
                {
                    time = string.Format("{0:X8}h", frameinfo.TimeStamp);
      
                id= frameinfo.ID;
              
                if (frameinfo.RemoteFlag == 0)
                {
                        RemoteFlag = "Data ";
                }
                else
                {
                        RemoteFlag = "Romte ";
                }
            
                if (frameinfo.ExternFlag == 0)
                {
                    tmpstr = "Stand ";
                }
                else
                {
                    tmpstr = "Exten ";
                }
               
                if (frameinfo.RemoteFlag == 0)
          
                    if (frameinfo.DataLen > 8)
                    {
                        frameinfo.DataLen = 8;
                    }
                    int mlen = frameinfo.DataLen - 1;
                    for (int j = 0; j <= mlen; j++)
                    {
                       temp[mCount][j] = frameinfo.data[j];
                      
                    }
                }
                

                mCount++;

                if (mCount >= 50)
                {
                    break;
                }


               
            }

            return temp;


        }


        public void disableCan() {
            if (m_connect != 0)
            {
                this.m_connect = 0;
                this.mCan.EnableProc = false;
                ECANDLL.CloseDevice(1, 0);
            }
        }

        ~CANTester() {
            if (m_connect != 0) { 
            this.m_connect = 0;
            this.mCan.EnableProc = false;
            ECANDLL.CloseDevice(1, 0);
            }
        }
    }


}
