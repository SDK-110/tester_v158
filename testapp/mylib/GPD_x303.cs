using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace testapp
{
    class GPD_x303
    {
        private PortOperatorBase GDP303Perator;
        public GPD_x303(string z,int bandrate) {

            GDP303Perator = PortUltility.serial_op(z,bandrate);
            System.Threading.Thread.Sleep(100);
            GDP303Perator.WriteLine("REMOTE");
            GDP303Perator.WriteLine("OUT0");


        }

        public void setcurrent(string ch, string value) {

            GDP303Perator.WriteLine("ISET" + ch + ":" + value);
        }
        public string getcurrent(string ch)
        {

           GDP303Perator.WriteLine("IOUT" + ch  + "?");

            return GDP303Perator.Read();
        }

        public void setvolatage(string ch, string value) {

            GDP303Perator.WriteLine("VSET" + ch + ":" + value);
        }


        public string getvolatage(string ch) {


            GDP303Perator.WriteLine("VOUT" + ch + "?");

            return GDP303Perator.Read();
        }

        public void OUTPUT()
        {


            GDP303Perator.WriteLine("VOUT1");

            
        }
        public void NOOUTPUT()
        {


            GDP303Perator.WriteLine("VOUT0");


        }


        ~GPD_x303() {

            GDP303Perator.WriteLine("OUT0");
            GDP303Perator.Close();

        }
    }
}
