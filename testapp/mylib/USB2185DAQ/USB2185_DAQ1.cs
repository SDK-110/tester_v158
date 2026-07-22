using Sys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testapp.mylib.USB2185DAQ
{

    class USB2185_DAQ1
    {
        [DllImport("msvcrt.dll")]
         static extern int _getch();
        [DllImport("msvcrt.dll")]
       static extern int _kbhit();
         const int WAIT_OBJECT_0 = 0;
        [DllImport("Kernel32.dll")]
         static extern int WaitForSingleObject(IntPtr hHandle, int dwMillisenconds);
        [DllImport("Kernel32.dll")]
        static extern IntPtr CreateEvent(String lpEventAttributes, bool bManualReset, bool bInitialState, String lpName);
          USB2185.USB2185_AI_PARAM AIParam;
         double PI2 = 6.28318531;
         static USB2185.USB2185_AO_PARAM AOParam;
         static USB2185.USB2185_AO_STATUS AOStatus;
        double[] fAnlgArray = new double[32768];
        IntPtr hDevice;
        UInt32 nReadSampsPerChan = 1024;
        UInt32 nSampsPerChanRead = 0;
        UInt32 nAvailSampsPerChan = 0;
        double fTimeout = 10.0;
        private  USB2185.USB2185_DIO_PARAM DIOParam;


        //output 用
        UInt32 nTotalSamps = 0;
        UInt32 nWriteSampsPerChan = 0;
        UInt32 nSampsPerChanWritten = 0;

        public USB2185_DAQ1()
        {

            hDevice = USB2185.USB2185_DEV_Create(0, false);
            if (hDevice == (IntPtr)(-1)) new Exception("DAQ error");


        }

        public void set_init_voltage_paraeter() {



            AIParam.nSampChanCount = 4;

            AIParam.CHParam0.nChannel = 0;
            AIParam.CHParam0.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam0.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam0.nReserved0 = 0;
            AIParam.CHParam0.nReserved1 = 0;
            AIParam.CHParam0.nReserved2 = 0;

            AIParam.CHParam1.nChannel = 1;
            AIParam.CHParam1.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam1.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam1.nReserved0 = 0;
            AIParam.CHParam1.nReserved1 = 0;
            AIParam.CHParam1.nReserved2 = 0;

            AIParam.CHParam2.nChannel = 2;
            AIParam.CHParam2.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam2.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam2.nReserved0 = 0;
            AIParam.CHParam2.nReserved1 = 0;
            AIParam.CHParam2.nReserved2 = 0;

            AIParam.CHParam3.nChannel = 3;
            AIParam.CHParam3.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam3.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam3.nReserved0 = 0;
            AIParam.CHParam3.nReserved1 = 0;
            AIParam.CHParam3.nReserved2 = 0;

            AIParam.CHParam4.nChannel = 4;
            AIParam.CHParam4.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam4.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam4.nReserved0 = 0;
            AIParam.CHParam4.nReserved1 = 0;
            AIParam.CHParam4.nReserved2 = 0;

            AIParam.CHParam5.nChannel = 5;
            AIParam.CHParam5.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam5.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam5.nReserved0 = 0;
            AIParam.CHParam5.nReserved1 = 0;
            AIParam.CHParam5.nReserved2 = 0;

            AIParam.CHParam6.nChannel = 6;
            AIParam.CHParam6.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam6.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam6.nReserved0 = 0;
            AIParam.CHParam6.nReserved1 = 0;
            AIParam.CHParam6.nReserved2 = 0;

            AIParam.CHParam7.nChannel = 7;
            AIParam.CHParam7.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam7.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam7.nReserved0 = 0;
            AIParam.CHParam7.nReserved1 = 0;
            AIParam.CHParam7.nReserved2 = 0;

            AIParam.CHParam8.nChannel = 8;
            AIParam.CHParam8.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam8.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam8.nReserved0 = 0;
            AIParam.CHParam8.nReserved1 = 0;
            AIParam.CHParam8.nReserved2 = 0;

            AIParam.CHParam9.nChannel = 9;
            AIParam.CHParam9.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam9.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam9.nReserved0 = 0;
            AIParam.CHParam9.nReserved1 = 0;
            AIParam.CHParam9.nReserved2 = 0;

            AIParam.CHParam10.nChannel = 10;
            AIParam.CHParam10.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam10.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam10.nReserved0 = 0;
            AIParam.CHParam10.nReserved1 = 0;
            AIParam.CHParam10.nReserved2 = 0;

            AIParam.CHParam11.nChannel = 11;
            AIParam.CHParam11.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam11.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam11.nReserved0 = 0;
            AIParam.CHParam11.nReserved1 = 0;
            AIParam.CHParam11.nReserved2 = 0;

            AIParam.CHParam12.nChannel = 12;
            AIParam.CHParam12.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam12.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam12.nReserved0 = 0;
            AIParam.CHParam12.nReserved1 = 0;
            AIParam.CHParam12.nReserved2 = 0;

            AIParam.CHParam13.nChannel = 13;
            AIParam.CHParam13.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam13.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam13.nReserved0 = 0;
            AIParam.CHParam13.nReserved1 = 0;
            AIParam.CHParam13.nReserved2 = 0;

            AIParam.CHParam14.nChannel = 14;
            AIParam.CHParam14.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam14.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam14.nReserved0 = 0;
            AIParam.CHParam14.nReserved1 = 0;
            AIParam.CHParam14.nReserved2 = 0;

            AIParam.CHParam15.nChannel = 15;
            AIParam.CHParam15.nSampleRange = USB2185.USB2185_AI_SAMPRANGE_N10_P10V;
            AIParam.CHParam15.nRefGround = USB2185.USB2185_AI_REFGND_RSE;
            AIParam.CHParam15.nReserved0 = 0;
            AIParam.CHParam15.nReserved1 = 0;
            AIParam.CHParam15.nReserved2 = 0;



            AIParam.nSampleSignal = USB2185.USB2185_AI_SAMPSIGNAL_AI;

            AIParam.nReserved0 = 0;
            AIParam.nReserved1 = 0;
            // 时钟参数
            AIParam.nSampleMode = USB2185.USB2185_AI_SAMPMODE_CONTINUOUS;
            AIParam.nSampsPerChan = 1024;
            AIParam.fSampleRate = 25000.0;
            AIParam.nClockSource = USB2185.USB2185_AI_CLKSRC_LOCAL;
            AIParam.bClockOutput = 0;
            AIParam.nReserved2 = 0;
            AIParam.nReserved3 = 0;

            // 开始触发参数
            AIParam.bDTriggerEn = 1;
            AIParam.nDTriggerDir = USB2185.USB2185_AI_TRIGDIR_FALLING;
            AIParam.bATriggerEn = 1;
            AIParam.nATriggerDir = USB2185.USB2185_AI_TRIGDIR_FALLING;
            AIParam.nATrigChannel = 0;
            AIParam.fTriggerLevel = 0;
            AIParam.nTriggerSens = 5;
            AIParam.nDelaySamps = 0;
            AIParam.nReserved4 = 0;
            AIParam.nReserved5 = 0;

            // 其他参数
            AIParam.nReserved6 = 0;
            AIParam.nReserved7 = 0;
            AIParam.nReserved8 = 0;
            AIParam.nReserved9 = 0;


        }
        public int set_init_voltage_read() {
            if (hDevice == null) return -1;
            set_init_voltage_paraeter();

            if (USB2185.USB2185_AI_VerifyParam(hDevice, ref AIParam) == false)
            {
                return -1;
            }

            // 第二步 初始化AI采集任务
            if (USB2185.USB2185_AI_InitTask(hDevice, ref AIParam, (IntPtr)null) == false)
            {
                return -2;
            }

            // 第三步 开始AI采集任务
            if (USB2185.USB2185_AI_StartTask(hDevice) == false)
            {
                return -3;
            }

            // 第四步 发送软件触发事件(硬件外触发时不需要)
            if (USB2185.USB2185_AI_SendSoftTrig(hDevice) == false)
            {
                return -4;
            }



            return 1;

        }

        public void set_out_wave_init() {



            AOParam.CHParam0.bChannelEn = 1;
            AOParam.CHParam0.nSampleRange = USB2185.USB2185_AO_SAMPRANGE_N10_P10V;
            AOParam.CHParam0.nReserved0 = 0;
            AOParam.CHParam0.nReserved1 = 0;
            AOParam.CHParam0.nReserved2 = 0;
            AOParam.CHParam0.nReserved3 = 0;

            AOParam.CHParam1.bChannelEn = 1;
            AOParam.CHParam1.nSampleRange = USB2185.USB2185_AO_SAMPRANGE_N10_P10V;
            AOParam.CHParam1.nReserved0 = 0;
            AOParam.CHParam1.nReserved1 = 0;
            AOParam.CHParam1.nReserved2 = 0;
            AOParam.CHParam1.nReserved3 = 0;

            AOParam.CHParam2.bChannelEn = 1;
            AOParam.CHParam2.nSampleRange = USB2185.USB2185_AO_SAMPRANGE_N10_P10V;
            AOParam.CHParam2.nReserved0 = 0;
            AOParam.CHParam2.nReserved1 = 0;
            AOParam.CHParam2.nReserved2 = 0;
            AOParam.CHParam2.nReserved3 = 0;

            AOParam.CHParam3.bChannelEn = 1;
            AOParam.CHParam3.nSampleRange = USB2185.USB2185_AO_SAMPRANGE_N10_P10V;
            AOParam.CHParam3.nReserved0 = 0;
            AOParam.CHParam3.nReserved1 = 0;
            AOParam.CHParam3.nReserved2 = 0;
            AOParam.CHParam3.nReserved3 = 0;
            // 时钟参数
            AOParam.nSampleMode = USB2185.USB2185_AO_SAMPMODE_CONTINUOUS;
            AOParam.nSampsPerChan = 1024;
            AOParam.fSampleRate = 50000.0;
            AOParam.nClockSource = USB2185.USB2185_AO_CLKSRC_LOCAL;
            AOParam.bClockOutput = 0;
            AOParam.bRegenModeEn = 1;

            // 开始触发参数
            AOParam.bDTriggerEn = 1;
            AOParam.nDTriggerDir = USB2185.USB2185_AO_TRIGDIR_FALLING;
            AOParam.nTriggerSens = 5;
            AOParam.nDelaySamps = 0;

            // 其他参数
            AOParam.nReserved0 = 0;
            AOParam.nReserved1 = 0;
            AOParam.nReserved2 = 0;







        }

        public int wave_output() {



            if (hDevice == null) return -1;

            if (USB2185.USB2185_AO_VerifyParam(hDevice, ref AOParam) == false)
            {
                return -1;
            }

            // 初始化AO生成任务
            if (USB2185.USB2185_AO_InitTask(hDevice, ref AOParam, (IntPtr)null) == false)
            {
                return -2;
            }
        

            nWriteSampsPerChan = AOParam.nSampsPerChan;
            nTotalSamps = AOParam.nSampsPerChan * 4;
            double[] fAnlgArray = new double[nTotalSamps];
            AO_CreateWave(fAnlgArray, AOParam);

            if (USB2185.USB2185_AO_WriteAnalog(hDevice, fAnlgArray, nWriteSampsPerChan, ref nSampsPerChanWritten, ref nAvailSampsPerChan, fTimeout) == false)
            {
                return -3;
            }

            // 开始AO生成任务
            if (USB2185.USB2185_AO_StartTask(hDevice) == false)
            {
                return -4;
            }

            // 发送软件强制触发(硬件触发时不需要)
            if (USB2185.USB2185_AO_SendSoftTrig(hDevice) == false)
            {
                return -5;
            }

            // 向AO生成任务继续写入数据
            while (_kbhit() == 0)
            {
                if (USB2185.USB2185_AO_GetStatus(hDevice, ref AOStatus) == false)
                {
                    return -6;
                 
                }
            //    Console.WriteLine("bTaskDone={0} bTriggered={1} nSampsPerChanAcquired={2}", AOStatus.bTaskDone, AOStatus.bTriggered, AOStatus.nSampsPerChanAcquired);
             //   Console.WriteLine("nSoftUnderflowCnt={0} nHardUnderflowCnt={1}", AOStatus.nSoftUnderflowCnt, AOStatus.nHardUnderflowCnt);
                Thread.Sleep(50);
            }





            return 1;




        }

        public int getvoltage(out Dictionary<UInt32, double> result, int select = 4) {
           
            ArrayList[] arraylist = new ArrayList[16];
            for (int i = 0; i < 16; i++) {

                arraylist[i] = new ArrayList();

            }
           
            Dictionary<UInt32, double> rsu = new Dictionary<UInt32, double>();
            result = rsu;
            if (hDevice == null) return -1;

            if (USB2185.USB2185_AI_ReadAnalog(hDevice, fAnlgArray, nReadSampsPerChan, ref nSampsPerChanRead, ref nAvailSampsPerChan, fTimeout) == false)
                {
                    return -1;
                }
                for (UInt32 nIndex = 0; nIndex < 64; nIndex++)
                {
                    for (UInt32 nChannel = 0; nChannel < 16; nChannel++)
                    {
                    arraylist[nChannel].Add(fAnlgArray[nChannel + nIndex * 4]);
                    }
                   
                }

            switch (select) {
                case 1:
                    for (int i = 0; i < 16; i++) {
                        rsu.Add((uint)i, (double)arraylist[i].ToArray().Max());
                    }
                    break;
                case 2:
                    for (int i = 0; i < 16; i++)
                    {
                        rsu.Add((uint)i, (double)arraylist[i].ToArray().Min());
                    }
                    break;
                case 3:
                    for (int i = 0; i < 16; i++)
                    {
                        rsu.Add((uint)i, (double)(arraylist[i].ToArray()).Average((o)=>{

                            return (double)o;
                        }));
                    }
                    break;
                case 4:
                    for (int i = 0; i < 16; i++)
                    {


                        arraylist[i].Remove(arraylist[i].ToArray().Max());
                        arraylist[i].Remove(arraylist[i].ToArray().Min());
                        rsu.Add((uint)i, (double)(arraylist[i].ToArray()).Average((o) => {

                            return (double)o;
                        }));
                    }



                    break;


            }
            
            result = rsu;
            return 1;
        }

        public int self_cab() {



            if (hDevice == null) return -1;
            if (USB2185RSV.USB2185_AI_SelfCal(hDevice) == false)
            {
                return -1;
            }
            else
            {
                return 1;
            }






        }


        public UInt32 AO_CreateWave(double[] fAnlgArray, USB2185.USB2185_AO_PARAM AOParam)
        {
            UInt32 nCyclePoints0 = 512;
            UInt32 nCyclePoints1 = 512;
            UInt32 nCyclePoints2 = 512;
            UInt32 nCyclePoints3 = 512;

            UInt32 nDataIndex = 0;
            for (UInt32 nIndex = 0; nIndex < AOParam.nSampsPerChan; nIndex++)
            {
                if (AOParam.CHParam0.bChannelEn == 1)
                {
                    fAnlgArray[nDataIndex] = Math.Sin(PI2 * (nIndex % nCyclePoints0) / nCyclePoints0) * 10.0;
                    nDataIndex++;
                }
                if (AOParam.CHParam1.bChannelEn == 1)
                {
                    fAnlgArray[nDataIndex] = Math.Sin(PI2 * (nIndex % nCyclePoints1) / nCyclePoints1) * 5.0;
                    nDataIndex++;
                }
                if (AOParam.CHParam2.bChannelEn == 1)
                {
                    fAnlgArray[nDataIndex] = Math.Sin(PI2 * (nIndex % nCyclePoints2) / nCyclePoints2) * 10.0;
                    nDataIndex++;
                }
                if (AOParam.CHParam3.bChannelEn == 1)
                {
                    fAnlgArray[nDataIndex] = Math.Sin(PI2 * (nIndex % nCyclePoints3) / nCyclePoints3) * 5.0;
                    nDataIndex++;
                }
            }
            return nDataIndex;
        }




        public void set_io_param(byte setp) {

            //byte setp =  Convert.ToByte(setpstr,2);
            DIOParam.bOutputEn0 =(byte)((setp &(byte)0x01<<0)>>0);     // 允许Line0输出
            DIOParam.bOutputEn1 = (byte)((setp & (byte)0x01 << 1) >> 1);     // 允许Line1输出
            DIOParam.bOutputEn2 = (byte)((setp & (byte)0x01 << 2) >> 2);     // 允许Line2输出
            DIOParam.bOutputEn3 = (byte)((setp & (byte)0x01 << 3) >> 3); ;     // 禁止Line3输出
            DIOParam.bOutputEn4 = (byte)((setp & (byte)0x01 << 4) >> 4); ;     // 禁止Line4输出
            DIOParam.bOutputEn5 = (byte)((setp & (byte)0x01 << 5) >> 5); ;     // 禁止Line5输出
            DIOParam.bOutputEn6 = (byte)((setp & (byte)0x01 << 6) >> 6); ;     // 禁止Line6输出
            DIOParam.bOutputEn7 = (byte)((setp & (byte)0x01 << 7) >> 7); ;     // 禁止Line7输出
            DIOParam.nReserved0 = 0;
            DIOParam.nReserved1 = 0;
            DIOParam.nReserved2 = 0;

        }

        public int set_io(uint port, uint ionumber ,uint hi_low ) {


            if (hDevice == null) return -1;

            if (USB2185.USB2185_DIO_WriteLine(hDevice, port, ionumber, hi_low) == false)
            {

                return -1;
            }

            //if (USB2185.USB2185_DIO_ReadLine(hDevice, nPort, nLine, ref bLineData) == false)
            //{
            //    return -1;
            //}


            return 1;

        }

        public int read_io(uint port, uint ionumber, ref uint hi_low) {

            if (hDevice == null) return -1;
            if (USB2185.USB2185_DIO_ReadLine(hDevice, port, ionumber, ref hi_low) == false)
            {
                return -1;
            }




            return 1;

        }

        public int set_io_init(uint ports ) {


            if (USB2185.USB2185_DIO_InitTask(hDevice, ports, ref DIOParam) == false)
            {
                return -1;
            }

            return 1;
        } 
            
            
         



    ~USB2185_DAQ1() {

            if (hDevice != (IntPtr)(-1))
            {
                if (USB2185.USB2185_AI_StopTask(hDevice) == false)
                {

                }

                // 第七步 释放AI采集任务
                if (USB2185.USB2185_AI_ReleaseTask(hDevice) == false)
                {

                }


                //// 第八步 释放设备对象
                //if (USB2185.USB2185_DEV_Release(hDevice) == false)
                //{

                //}

                // 停止AO生成任务
                if (USB2185.USB2185_AO_StopTask(hDevice) == false)
                {

                }

                // 释放AO生成任务
                if (USB2185.USB2185_AO_ReleaseTask(hDevice) == false)
                {

                }






           

                // 释放设备对象
                if (USB2185.USB2185_DEV_Release(hDevice) == false)
                {
                 
                }

            }
        }
    }
}
