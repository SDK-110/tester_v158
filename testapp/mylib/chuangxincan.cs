using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace testapp
{
    #region 数据结构
    public struct VCI_BOARD_INFO
    {
        public UInt16 hw_Version;
        public UInt16 fw_Version;
        public UInt16 dr_Version;
        public UInt16 in_Version;
        public UInt16 irq_Num;
        public byte can_Num;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] str_Serial_Num;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] str_hw_Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved;
    }

    /////////////////////////////////////////////////////
    //2.定义CAN信息帧的数据类型。
    unsafe public struct VCI_CAN_OBJ  //使用不安全代码
    {
        public uint ID;
        public uint TimeStamp;        //时间标识
        public byte TimeFlag;         //是否使用时间标识
        public byte SendType;         //发送标志。保留，未用
        public byte RemoteFlag;       //是否是远程帧
        public byte ExternFlag;       //是否是扩展帧
        public byte DataLen;          //数据长度
        public fixed byte Data[8];    //数据
        public fixed byte Reserved[3];//保留位

    }

    //3.定义初始化CAN的数据类型
    public struct VCI_INIT_CONFIG
    {
        public UInt32 AccCode;
        public UInt32 AccMask;
        public UInt32 Reserved;
        public byte Filter;   //0或1接收所有帧。2标准帧滤波，3是扩展帧滤波。
        public byte Timing0;  //波特率参数，具体配置，请查看二次开发库函数说明书。
        public byte Timing1;
        public byte Mode;     //模式，0表示正常模式，1表示只听模式,2自测模式
    }

    /*------------其他数据结构描述---------------------------------*/
    //4.USB-CAN总线适配器板卡信息的数据类型1，该类型为VCI_FindUsbDevice函数的返回参数。
    public struct VCI_BOARD_INFO1
    {
        public UInt16 hw_Version;
        public UInt16 fw_Version;
        public UInt16 dr_Version;
        public UInt16 in_Version;
        public UInt16 irq_Num;
        public byte can_Num;
        public byte Reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] public byte[] str_Serial_Num;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] str_hw_Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] str_Usb_Serial;
    }

    /*------------数据结构描述完成---------------------------------*/

    public struct CHGDESIPANDPORT
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] szpwd;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] szdesip;
        public Int32 desport;

        public void Init()
        {
            szpwd = new byte[10];
            szdesip = new byte[20];
        }
    }

    public struct VCI_ERR_INFO
    {

        /// unsigned int
        public uint ErrCode;

        /// unsigned char[3]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 3)]
        public string Passive_ErrData;

        /// unsigned char
        public byte ArLost_ErrData;
    }


    #endregion








  class chuangxincan
    {
     

        #region ---------------dll载入-------------------------

         /// <summary>
         /// 
         /// </summary>
         /// <param name="DeviceType"></param>
         /// <param name="DeviceInd"></param>
         /// <param name="Reserved"></param>
         /// <returns></returns>
         /*------------兼容ZLG的函数描述---------------------------------*/
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ReadBoardInfo(UInt32 DeviceType, UInt32 DeviceInd, ref VCI_BOARD_INFO pInfo);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_GetReceiveNum(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ClearBuffer(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ResetCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ReadErrInfo(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_ERR_INFO pERR);

        /*------------其他函数描述---------------------------------*/

        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_ConnectDevice(UInt32 DevType, UInt32 DevIndex);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_UsbDeviceReset(UInt32 DevType, UInt32 DevIndex, UInt32 Reserved);
        [DllImport("controlcan.dll")]
        static extern UInt32 VCI_FindUsbDevice(ref VCI_BOARD_INFO1 pInfo);
        /*------------函数描述结束---------------------------------*/





        #endregion


        // const int DEV_USBCAN = 3;
        //  const int DEV_USBCAN2 = 4;
        private UInt32 m_devtype = 4;//USBCAN2
        private UInt32 m_devind = 0;
        private UInt32 m_canind = 0;



        private  UInt32 m_bOpen = 0;
        VCI_CAN_OBJ[] m_recobj = new VCI_CAN_OBJ[1000];
        VCI_ERR_INFO m_err = new VCI_ERR_INFO();
        UInt32[] m_arrdevtype = new UInt32[20];
        VCI_INIT_CONFIG config = new VCI_INIT_CONFIG();
      
        public  chuangxincan(int baudrate = 33333, UInt32 AccCode = 0, UInt32 AccMask = 0xffffffff,
                           byte Filter = 1, byte Mode = 0, UInt32 devtype = 3, UInt32 devind = 0, UInt32 canind=0) {
            m_devtype = devtype;
            m_devind = devind;
            m_canind = canind;
            //if (VCI_OpenDevice(m_devtype, m_devind, 0) == 0) {

            //    System.Windows.Forms.MessageBox.Show("CAN div OPEN ERROR");
            //    throw new Exception("CAN div OPEN ERROR");
            //}
            m_bOpen = 1;

            switch (baudrate)
            {
                case 1000: //1000

                    config.Timing0 = 0;
                    config.Timing1 = 0x14;
                    break;
                case 800: //800

                    config.Timing0 = 0;
                    config.Timing1 = 0x16;
                    break;
                case 666: //666

                    config.Timing0 = 0x80;
                    config.Timing1 = 0xb6;
                    break;
                case 500: //500

                    config.Timing0 = 0;
                    config.Timing1 = 0x1c;
                    break;
                case 400://400

                    config.Timing0 = 0x80;
                    config.Timing1 = 0xfa;
                    break;
                case 250://250

                    config.Timing0 = 0x01;
                    config.Timing1 = 0x1c;
                    break;
                case 200://200

                    config.Timing0 = 0x81;
                    config.Timing1 = 0xfa;
                    break;
                case 125://125

                    config.Timing0 = 0x03;
                    config.Timing1 = 0x1c;
                    break;
                case 100://100

                    config.Timing0 = 0x04;
                    config.Timing1 = 0x1c;
                    break;
                case 80://80

                    config.Timing0 = 0x83;
                    config.Timing1 = 0xff;
                    break;
                case 50://50

                    config.Timing0 = 0x09;
                    config.Timing1 = 0x1c;
                    break;
                case 126984://126984

                    config.Timing0 = 0x02;
                    config.Timing1 = 0x3f;
                    break;
                case 33333://126984

                    config.Timing0 = 0x09;
                    config.Timing1 = 0x6F;
                    break;

            }
            try
            {
               
                if (VCI_OpenDevice(m_devtype, m_devind, 0) == 0)
                {
                    VCI_ResetCAN(m_devtype, m_devind, m_canind);
                    // System.Windows.Forms.MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确");
                   // return;
                }
            }
            catch { }
           
            config.AccCode = AccCode;
            config.AccMask = AccMask;
            config.Filter =Filter;
            config.Mode = Mode;
            VCI_InitCAN(m_devtype, m_devind, m_canind, ref config);
            System.Threading.Thread.Sleep(200);
            if (VCI_StartCAN(m_devtype, m_devind, m_canind) != 1) {

                System.Windows.Forms.MessageBox.Show("canNalyst-II can adapter open error ,please reopen test app");
            };

        }

        public void startcan() {

            if (m_bOpen == 0)
                return;
            VCI_StartCAN(m_devtype, m_devind, m_canind);
        }

        public void stopcan() {

          if (m_bOpen == 0)
                return;
            VCI_ResetCAN(m_devtype, m_devind, m_canind);

        }

        public bool senddata( byte[] sendData,byte FrameFormat=0, byte FrameType=0, UInt32 ID=0x181) {

            VCI_CAN_OBJ sendobj = new VCI_CAN_OBJ();

            sendobj.RemoteFlag = FrameFormat;
            sendobj.ExternFlag = FrameType;
            sendobj.ID = ID;
            sendobj.DataLen = System.Convert.ToByte(sendData.Length);
           unsafe 
             {
                for(int i = 0; i < sendData.Length; i++)
                {
                    sendobj.Data[i] = sendData[i];

                }

            }

            if (VCI_Transmit(m_devtype, m_devind, m_canind, ref sendobj, 1) == 0)
                return false;
        
            
            return true;
        }


        public int  send_rev(byte[] send_byte_data,out uint response_id,out byte [] result,uint target_id= 0x181) {

            startcan();
            result = new byte[] { };
            response_id = 0;
            if (send_byte_data.Length > 8) return -1;
            for(int i = 0; i < 2; i++) { 
            if (senddata(send_byte_data, 0, 0, target_id) == true) {
                    System.Threading.Thread.Sleep(100);
                    break;
            };
            }
            bool flog = false;
            uint rtid=0;
            byte[] result_ret = new byte[] { };
           
            //  VCI_CAN_OBJ rsu= new VCI_CAN_OBJ();
            for (int i = 0; i < 10; i++) {
               System.Threading.Thread.Sleep(100);
                if (datarev(out rtid,out result_ret)!=1){;
              
                   // System.Threading.Thread.Sleep(100);
                    continue;
            }
            else break;


                if (i == 9) return -2;
            }

            if (rtid == 0) return -3;
            response_id = rtid;

            result = result_ret;

            stopcan();
            return 1;

        }

        public int datarev(out uint id, out byte[] data) {

          
            id = 0;
            data = new byte[] { };
            UInt32 res = VCI_Receive(m_devtype, m_devind, m_canind, ref m_recobj[0], 1000, 100);


           
          if (res == 0xFFFFFFFF || res==0) {
                res = 0;//当设备未初始化时，返回0xFFFFFFFF，不进行列表显示。
                VCI_ReadErrInfo(m_devtype, m_devind, m_canind, ref m_err);
               
                return -1;
                
            }

        

            unsafe { 
            fixed (VCI_CAN_OBJ* m_recobj1 = &m_recobj[0])
            {
                  data = new byte[m_recobj1->DataLen];
                  for(int i =0;i<m_recobj1->DataLen;i++)
                    { 
                    data[i] = m_recobj1->Data[i];
                    }
                    id = m_recobj1->ID;
                

            }
        }

            return 1;







    }



        ~chuangxincan() {

            if (m_bOpen == 1)
            {
                VCI_CloseDevice(m_devtype, m_devind);
                m_bOpen = 0;
            }




        }








    }
}
