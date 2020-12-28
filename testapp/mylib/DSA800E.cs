using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace testapp
{
   
 public    class DSA800E
    {
         private PortOperatorBase DSA800EPerator;
        public   DSA800E(string devicename) {


            //{
            
            DSA800EPerator = PortUltility.usbport_op(devicename);

           
            try
            {


                DSA800EPerator.WriteLine("*IDN?");
                string v = DSA800EPerator.ReadLine();
             
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("万用表没有连接好或资源端口设置不对");
            }


        }



    
        public   DSA800E(String devicename, int baudrate) {

            
                //{
                DSA800EPerator = PortUltility.serial_op(devicename, baudrate);

            try
            {

                
                DSA800EPerator.WriteLine("*IDN?");
                string v = DSA800EPerator.ReadLine();

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("DSA832E SPEC IS NOT CONNECTED GOOD ");
            }





        }

        public void reset() {

            DSA800EPerator.WriteLine("*RST");
            DSA800EPerator.WriteLine("*cls");
        }


        public bool MaxPowerRead(  out int Maxfreq,
                                   out Double Maxpower,
                                   out int Freqdev,
                                   int freqcenter,
                                   UInt32 SPAN=20000000/*20M*/,
                                   UInt32 RBW=1000000/*设置分辨率带宽（RBW）。*/,
                                   UInt32 VBW=1000000/*设置分辨率带宽（VBW）。*/,
                                   int scancount=10 /*测量的平均次数*/,
                                   int sweeptime=3/*单位为S*/,
                                   Double reflevel=10/*dbm*/) {

            DSA800EPerator.WriteLine($":SENS:FREQ:CENT {freqcenter}");
            DSA800EPerator.WriteLine($":SENSe:FREQuency:SPAN {SPAN}");
            DSA800EPerator.WriteLine($":SENSe:BANDwidth:RESolution:AUTO OFF");
            DSA800EPerator.WriteLine($":SENSe:BANDwidth:VIDeo:AUTO OFF");
            DSA800EPerator.WriteLine($":SENSe:BANDwidth:RESolution {RBW}");
            DSA800EPerator.WriteLine($":SENSe:BANDwidth:VIDeo {VBW}");
            DSA800EPerator.WriteLine($":SENSe:SWEep:COUNt {scancount}");
            DSA800EPerator.WriteLine($":INITiate:CONTinuous 0"); /*查询扫描或测量的方式。*/
            DSA800EPerator.WriteLine($":SENSe:SWEep:TIME {sweeptime}");
            DSA800EPerator.WriteLine($":CALCulate:MARKer1:PEAK:SEARch:MODE MAXimum"); 
            DSA800EPerator.WriteLine($":INITiate:IMMediate");
            System.Threading.Thread.Sleep(sweeptime + 1);
            DSA800EPerator.WriteLine($":CALCulate:MARKer1:MAXimum:MAX");

            Double maxpower =-888;
            int maxfreq = -888888888;
            int freqdev=-88888888;
            try{
                DSA800EPerator.WriteLine($":CALCulate:MARKer1:X?");
              string v = DSA800EPerator.ReadLine();
              maxfreq = int.Parse(v.Trim());

                DSA800EPerator.WriteLine($":CALCulate:MARKer<n>:Y?");
              string v2 =DSA800EPerator.ReadLine();
              maxpower = Double.Parse(v2.Trim());
             
               Maxfreq = maxfreq;
               Maxpower = maxpower;
               Freqdev = freqcenter - Maxfreq;
               return true;

            }catch{

                Maxfreq = -888888888;
                Maxpower = -888888888;
                Freqdev = -888888888;

                return false;
            
            }


            
     
           


        }
     

       

        ~DSA800E() {


            DSA800EPerator.Close();
        }

    }
}
