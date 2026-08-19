using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testapp.mylib.smacq_daq;
namespace testapp.mylib.smacq_daq
{
    enum set_ai_range { set_10 = 10, set_5 = -5 }
    class USB_12521_DAQ
    {
        ushort chansel = 0;
        int DevIndex = 1;
        int AiRange = 10;
        public USB_12521_DAQ()
        {

            DevIndex = usb_1000_LIB.FindUSBDAQ();

            usb_1000_LIB.OpenDevice(DevIndex);
            usb_1000_LIB.ResetDevice(DevIndex);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range">-5,10</param>
        /// <returns></returns>
        public int set_range(int range = 10)
        {

            try
            {
                usb_1000_LIB.SetUSB1AiRange(DevIndex, range);

                return 1;
            }
            catch
            {

                return -1;
            }
        }

        public int set_samplerate(uint samplerate = 100000)
        {
            try
            {
                if (usb_1000_LIB.SetWaveSampleRate(DevIndex, samplerate) != 0) return -2;

            }
            catch
            {
                return -1;
            }
            return 1;

        }

        public int get_ai_chans(uint num, ushort chsel, ref float ai, int time = 2000)
        {




            try
            {
                if (usb_1000_LIB.GetAiChans(DevIndex, num, chsel, ref ai, time) != 0) return -2;

            }
            catch
            {
                return -1;
            }
            return 1;











        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">1,0,3</param>
        /// <returns></returns>
        public int set_chanmode(byte mode)
        {
            try
            {
                if (usb_1000_LIB.SetChanMode(DevIndex, mode) != 0) return -2;

            }
            catch
            {


                return -1;

            }

            return 1;
        }

        public int set_init_DIO_IN_OUT_PUT(ushort setchan)
        {
            try
            {
                if (usb_1000_LIB.SetChanSel(DevIndex, setchan) != 0) return -2;




            }
            catch
            {

                return -1;
            }
            return 1;

        }

        public int set_init_counter_(byte ctrnum, byte ctrmode, byte ctredge)
        {

            try
            {

                if (usb_1000_LIB.SetCounter(DevIndex, ctrnum, ctrmode, ctredge) != 0) return -2;
            }
            catch
            {

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trig">0 close,1 open</param>
        /// <returns></returns>
        public int set_init_softtrig(byte trig)
        {
            try
            {

                if (usb_1000_LIB.SetSoftTrig(DevIndex, trig) != 0) return -2;
            }
            catch
            {

                return -1;
            }

            return 1;






        }

        public int set_dio_output(uint DIOOUT)
        {

            try
            {

                if (usb_1000_LIB.SetDioOut(DevIndex, DIOOUT) != 0) return -2;
            }
            catch
            {

                return -1;
            }

            return 1;



        }
        public int get_dio_status(ref uint rsu)
        {

            try
            {

                rsu = usb_1000_LIB.GetDioIn(DevIndex);
            }
            catch
            {

                return -1;
            }

            return 1;


        }


        public int set_transdioin(byte transdioswitch)
        {

            try
            {

                if (usb_1000_LIB.TransDioIn(DevIndex, transdioswitch) != 0) return -2;
            }
            catch
            {

                return -1;
            }

            return 1;



        }

        public int set_DAQ_run()
        {

            try
            {

                if (usb_1000_LIB.StartRead(DevIndex) != 0) return -2;


            }
            catch
            {

                return -1;
            }
            return 1;


        }

        public int set_DAQ_stop()
        {

            try
            {

                if (usb_1000_LIB.StopRead(DevIndex) != 0) return -2;


            }
            catch
            {

                return -1;
            }
            return 1;


        }

        ~USB_12521_DAQ()
        {




        }


    }
}
