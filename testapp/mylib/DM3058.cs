using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace testapp
{
   
 public    class DM3058
    {
         private PortOperatorBase DM3058OPerator;
        public   DM3058(string devicename) {


            //{
            
            DM3058OPerator = PortUltility.usbport_op(devicename);

           
            try
            {


                DM3058OPerator.WriteLine("CMDSET RIGOL");
                DM3058OPerator.WriteLine("*IDN?");
                string v = DM3058OPerator.ReadLine();
             
            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("万用表没有连接好或资源端口设置不对");
            }


        }



    
        public   DM3058(String devicename, int baudrate) {

            
                //{
                DM3058OPerator = PortUltility.serial_op(devicename, baudrate);

            try
            {

                DM3058OPerator.WriteLine("CMDSET RIGOL");
                DM3058OPerator.WriteLine("*IDN?");
                string v = DM3058OPerator.ReadLine();

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("DM3058万用表没有连接好或资源端口设置不对");
            }





        }

        public void reset() {

            DM3058OPerator.WriteLine("*RST");
            DM3058OPerator.WriteLine("cmdset rigol");
            DM3058OPerator.WriteLine("*cls");
        }
        public void Close()
        {
            
            DM3058OPerator.Close();
        }

        public bool isOpen() {

            return DM3058OPerator.IsPortOpen;
        }
        public string read_capactance() {


            DM3058OPerator.WriteLine(":MEASure:CAPacitance?");
     
            return DM3058OPerator.ReadLine();


        }
        public string read_resistance()
        {
            DM3058OPerator.WriteLine(":function:resistance");
            DM3058OPerator.WriteLine(":measure auto");
            DM3058OPerator.WriteLine(":measure:resistance?");

            return DM3058OPerator.ReadLine();

        }

        public string read_resistance(int range)
        {
            DM3058OPerator.WriteLine(":function:resistance");
            DM3058OPerator.WriteLine(":measure:resistance " + range);
            DM3058OPerator.WriteLine(":measure:resistance?");

            return DM3058OPerator.ReadLine();

        }

        public string read_resistance_4w()
        {
            DM3058OPerator.WriteLine(":FUNCtion:FRESistance");
            DM3058OPerator.WriteLine(":measure auto");
            DM3058OPerator.WriteLine(":MEASure:FRESistance?");

            return DM3058OPerator.ReadLine();

        }

        public string read_resistance_4w(int range)
        {
            DM3058OPerator.WriteLine(":FUNCtion:FRESistance");
            DM3058OPerator.WriteLine(":FUNCtion:FRESistance " + range);
            DM3058OPerator.WriteLine(":MEASure:FRESistance?");

            return DM3058OPerator.ReadLine();

        }

        public  string dm3058_dc_read_20v() {
            DM3058OPerator.Write(":RATE:VOLTage:DC M");
            System.Threading.Thread.Sleep(100);
        
            DM3058OPerator.Write(":MEASure:VOLTage:DC?");
            return DM3058OPerator.Read();
        }
        public  string dm3058_dc_read_200mv()
        {
            DM3058OPerator.Write(":RATE:VOLTage:DC M");
            
            DM3058OPerator.Write(":MEASure:VOLTage:DC 0");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:VOLTage:DC?");
            return DM3058OPerator.Read();
        }
        public  string dm3058_dc_read_200v()
        {
            DM3058OPerator.Write(":RATE:VOLTage:DC M");
            DM3058OPerator.Write(":MEASure:VOLTage:DC 3");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:VOLTage:DC?");
            return DM3058OPerator.Read();
        }

        public  string dm3058_dc_read_2mA()
        {
            DM3058OPerator.Write(":RATE:CURRent:DC M");
            DM3058OPerator.Write(":MEASure:CURRent:DC 1");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:CURRent:DC?");
            return DM3058OPerator.Read();
        }
        public  string dm3058_dc_read_20mA()
        {
            DM3058OPerator.Write(":RATE:CURRent:DC M");
            DM3058OPerator.Write(":MEASure:CURRent:DC 2");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:CURRent:DC?");
            return DM3058OPerator.Read();
        }

        public  string dm3058_dc_read_200mA()
        {
            DM3058OPerator.Write(":RATE:CURRent:DC M");
            DM3058OPerator.Write(":MEASure:CURRent:DC 3");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:CURRent:DC?");
            return DM3058OPerator.Read();
        }

        public  string dm3058_dc_read_10A()
        {
            DM3058OPerator.Write(":RATE:CURRent:DC M");
            DM3058OPerator.Write(":MEASure:CURRent:DC 5");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:CURRent:DC?");
            return DM3058OPerator.Read();
        }

        public  string dm3058_ac_read_20v()
        {
            DM3058OPerator.Write(":RATE:VOLTage:AC M");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:VOLTage:AC?");
            return DM3058OPerator.Read();
        }
        public  string dm3058_ac_read_200mv()
        {
            DM3058OPerator.Write(":RATE:VOLTage:AC M");
            DM3058OPerator.Write(":MEASure:VOLTage:AC 0");
            System.Threading.Thread.Sleep(100);

            DM3058OPerator.Write(":MEASure:VOLTage:AC?");
            return DM3058OPerator.Read();
        }
        public  string dm3058_ac_read_200v()
        {
            DM3058OPerator.Write(":RATE:VOLTage:AC M");
            DM3058OPerator.Write(":MEASure:VOLTage:AC 3");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:VOLTage:AC?");
            return DM3058OPerator.Read();
        }

        public  string dm3058_ac_read_750v()
        {
            DM3058OPerator.Write(":RATE:VOLTage:AC M");
            DM3058OPerator.Write(":MEASure:VOLTage:AC 4");
            System.Threading.Thread.Sleep(100);
            DM3058OPerator.Write(":MEASure:VOLTage:AC?");
            return DM3058OPerator.Read();
        }

        ~DM3058() {


            DM3058OPerator.Close();
        }

    }
}
