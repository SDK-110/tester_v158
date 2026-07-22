using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.mylib.smacq_daq
{
   public static  class usb_1000_LIB
    {


        public  const int NO_USBDAQ = -1;
        public const int DevIndex_Overflow = -2;
        public const int Bad_Firmware = -3;
        public const int USBDAQ_Closed = -4;
        public const int Transfer_Data_Fail = -5;
        public const int NO_Enough_Memory = -6;
        public const int Time_Out = -7;
        public const int Not_Reading = -8;


        /// Return Type: int
        [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "FindUSBDAQ")]
            public static extern int FindUSBDAQ();


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "OpenDevice")]
            public static extern int OpenDevice(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "ResetDevice")]
            public static extern int ResetDevice(int DevIndex);


            /// Return Type: void
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "CloseDevice")]
            public static extern void CloseDevice(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            ///Range: float
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetUSB1AiRange")]
            public static extern int SetUSB1AiRange(int DevIndex, float Range);


            /// Return Type: int
            ///DevIndex: int
            ///SampleRate: unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetSampleRate")]
            public static extern int SetSampleRate(int DevIndex, uint SampleRate);


            /// Return Type: int
            ///DevIndex: int
            ///ChSel: unsigned short
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetChanSel")]
            public static extern int SetChanSel(int DevIndex, ushort ChSel);


            /// Return Type: int
            ///DevIndex: int
            ///ChanMode: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetChanMode")]
            public static extern int SetChanMode(int DevIndex, byte ChanMode);


            /// Return Type: int
            ///DevIndex: int
            ///TrigSource: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetTrigSource")]
            public static extern int SetTrigSource(int DevIndex, byte TrigSource);


            /// Return Type: int
            ///DevIndex: int
            ///Trig: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetSoftTrig")]
            public static extern int SetSoftTrig(int DevIndex, byte Trig);


            /// Return Type: int
            ///DevIndex: int
            ///TrigEdge: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetTrigEdge")]
            public static extern int SetTrigEdge(int DevIndex, byte TrigEdge);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "ClearTrigger")]
            public static extern int ClearTrigger(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            ///DioOut: unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetDioOut")]
            public static extern int SetDioOut(int DevIndex, uint DioOut);


            /// Return Type: int
            ///DevIndex: int
            ///TransDioSwitch: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "TransDioIn")]
            public static extern int TransDioIn(int DevIndex, byte TransDioSwitch);


            /// Return Type: int
            ///DevIndex: int
            ///CtrNum: unsigned char
            ///CtrMode: unsigned char
            ///CtrEdge: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetCounter")]
            public static extern int SetCounter(int DevIndex, byte CtrNum, byte CtrMode, byte CtrEdge);


            /// Return Type: int
            ///DevIndex: int
            ///CtrNum: unsigned char
            ///OnOff: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "StartCounter")]
            public static extern int StartCounter(int DevIndex, byte CtrNum, byte OnOff);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "InitDA")]
            public static extern int InitDA(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            ///DANum: unsigned char
            ///DAVolt: float
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetDA")]
            public static extern int SetDA(int DevIndex, byte DANum, float DAVolt);


            /// Return Type: int
            ///DevIndex: int
            ///DANum: unsigned char
            ///DAVolt: float
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetWavePt")]
            public static extern int SetWavePt(int DevIndex, byte DANum, float DAVolt);


            /// Return Type: int
            ///DevIndex: int
            ///DANum: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "ClrWavePt")]
            public static extern int ClrWavePt(int DevIndex, byte DANum);


            /// Return Type: int
            ///DevIndex: int
            ///WaveSampleRate: unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "SetWaveSampleRate")]
            public static extern int SetWaveSampleRate(int DevIndex, uint WaveSampleRate);


            /// Return Type: int
            ///DevIndex: int
            ///DANum: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "WaveOutput")]
            public static extern int WaveOutput(int DevIndex, byte DANum);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "ClearBufs")]
            public static extern int ClearBufs(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            ///CtrNum: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "ClearCounter")]
            public static extern int ClearCounter(int DevIndex, byte CtrNum);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "StartRead")]
            public static extern int StartRead(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "StopRead")]
            public static extern int StopRead(int DevIndex);


            /// Return Type: int
            ///DevIndex: int
            ///Num: unsigned int
            ///ChSel: unsigned short
            ///Ai: float*
            ///TimeOut: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "GetAiChans")]
            public static extern int GetAiChans(int DevIndex, uint Num, ushort ChSel, ref float Ai, int TimeOut);


            /// Return Type: unsigned int
            ///DevIndex: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "GetDioIn")]
            public static extern uint GetDioIn(int DevIndex);


            /// Return Type: unsigned int
            ///DevIndex: int
            ///CtrNum: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "GetCounter")]
            public static extern uint GetCounter(int DevIndex, byte CtrNum);


            /// Return Type: double
            ///DevIndex: int
            ///CtrNum: unsigned char
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "GetCtrTime")]
            public static extern double GetCtrTime(int DevIndex, byte CtrNum);


            /// Return Type: int
            ///DevIndex: int
            ///Code: int
            [System.Runtime.InteropServices.DllImportAttribute("usb-1000.dll", EntryPoint = "GotoCalibrate")]
            public static extern int GotoCalibrate(int DevIndex, int Code);

        

    }
}
