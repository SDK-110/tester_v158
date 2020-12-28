using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace ClassLibrary1
{
    class led_assy_self : SerialPort
    {
        string recebuf;
        public led_assy_self(string port, int baudrate=57600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;
 
            base.WriteTimeout = 2000;
            base.ReadTimeout = 3000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region /*废弃*/
        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

        public string read()
        {

            string a = (recebuf == null) ? "null" : recebuf;
            recebuf = "";
            return a;
        }

        private void set_rly(byte[] m)
        {

            if (m.Length == 3)
            {

                this.send(new Byte[] { 0XB1, 0X03 });
                this.send(m);
                this.send(new Byte[] { 0X0A });

            }

        }
        private void getcolor(byte ch,byte cor) {

            this.send(new Byte[] {0xA5,0X04 });

            this.send(new byte[] { ch, cor });
            this.send(new byte[] { 0x00, 0x00, 0x0A });


        }

        private UInt16 crc(Byte [] data) {

          UInt16 a = 0;
            for (int i = 0; i < data.Length; i++) {

                a += (UInt16)data[i];
            
            }
            a %= 0x100;
            return a;
        }

        private Byte[] tran_crc(Byte[] data) {

            Byte[] a = data;
            UInt16 cr = crc(data);
            a[a.Length - 1] = (byte)cr;

            return a;
        }
        #endregion



        public void try_comm() {

            this.Write(new byte[] { 0 }, 0, 1);

            string m = this.ReadLine();


        }

        public int[][] getRGB() {
             int[][] temp = new int[][] { new int[] {0,0,0 },
                                         new int[] { 0,0,0 },
                                         new int[] { 0,0,0 },
                                         new int[] { 0,0,0 }
                                        };
            string[] rtnfx;
            int count = 0;
            do
            {
                this.Write(new byte[] { 0 }, 0, 1);
                string rtn = this.ReadLine();
                if(count > 3){

                    return temp;

                }
                count++;
                rtnfx = rtn.Split(";".ToArray());
                
            } while(rtnfx.Length!=3);

            temp[0][0] =  int.Parse(rtnfx[0].Split(",".ToCharArray())[0]);
            temp[0][1] = int.Parse(rtnfx[0].Split(",".ToCharArray())[1]);
            temp[0][2] = int.Parse(rtnfx[0].Split(",".ToCharArray())[2]);
            temp[1][0] = int.Parse(rtnfx[1].Split(",".ToCharArray())[0]);
            temp[1][1] = int.Parse(rtnfx[1].Split(",".ToCharArray())[1]);
            temp[1][2] = int.Parse(rtnfx[1].Split(",".ToCharArray())[2]);
            temp[2][0] = int.Parse(rtnfx[2].Split(",".ToCharArray())[0]);
            temp[2][1] = int.Parse(rtnfx[2].Split(",".ToCharArray())[1]);
            temp[2][2] = int.Parse(rtnfx[2].Split(",".ToCharArray())[2]);
            temp[3][0] = int.Parse(rtnfx[3].Split(",".ToCharArray())[0]);
            temp[3][1] = int.Parse(rtnfx[3].Split(",".ToCharArray())[1]);
            temp[3][2] = int.Parse(rtnfx[3].Split(",".ToCharArray())[2]);

            return temp;
        }
        public int[] sign_sensor_comp(int z) {

            int[][] temp = getRGB();


            return temp[z];
        }
        public int[][] getRGB_peak_0000to2000(int millisecond)
        {
            int[][] temp = new int[][] { new int[] {0,0,0 },
                                         new int[] { 0,0,0 },
                                         new int[] { 0,0,0 },
                                         new int[] { 0,0,0 }
                                        };
            string[] rtnfx;
            int count = 0;
            do
            {
                this.Write("@judge");
                string rtn;
                try
                {
                    rtn = this.ReadLine();
                }
                catch (Exception) {

                    rtn = "-1,-1,-1;-1,-1,-1;-1,-1,-1;-1,-1,-1";
                }
                if (count > 3)
                {

                    return temp;

                }
                count++;
                rtnfx = rtn.Split(";".ToArray());

            } while (rtnfx.Length != 3);

            temp[0][0] = int.Parse(rtnfx[0].Split(",".ToCharArray())[0]);
            temp[0][1] = int.Parse(rtnfx[0].Split(",".ToCharArray())[1]);
            temp[0][2] = int.Parse(rtnfx[0].Split(",".ToCharArray())[2]);
            temp[1][0] = int.Parse(rtnfx[1].Split(",".ToCharArray())[0]);
            temp[1][1] = int.Parse(rtnfx[1].Split(",".ToCharArray())[1]);
            temp[1][2] = int.Parse(rtnfx[1].Split(",".ToCharArray())[2]);
            temp[2][0] = int.Parse(rtnfx[2].Split(",".ToCharArray())[0]);
            temp[2][1] = int.Parse(rtnfx[2].Split(",".ToCharArray())[1]);
            temp[2][2] = int.Parse(rtnfx[2].Split(",".ToCharArray())[2]);
            temp[3][0] = int.Parse(rtnfx[3].Split(",".ToCharArray())[0]);
            temp[3][1] = int.Parse(rtnfx[3].Split(",".ToCharArray())[1]);
            temp[3][2] = int.Parse(rtnfx[3].Split(",".ToCharArray())[2]);

            return temp;
        }



        ~led_assy_self()
        {
            this.Close();
        }

    }

}

