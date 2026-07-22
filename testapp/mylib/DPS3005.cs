using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class DPS3005 : SerialPort
    {
        string recebuf;
        public DPS3005(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;

            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }


        public double[] read_voltage_curent() {

            this.DiscardInBuffer();
            try
            {
                byte[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
               for (int i = 0; i < 3; i++)
               {
                    for (int cc = 0; cc < 9; cc++) result[cc] = 0;
                   
                    System.Threading.Thread.Sleep(100);
                    // System.Threading.Thread.Sleep(20);
                    byte[] readcode = { 0x01, 0x03, 0x00, 0x02, 0x00, 0x02, 0x65, 0xcb };
                    this.Write(readcode, 0, readcode.Length);
                    System.Threading.Thread.Sleep(400);                  
                    this.Read(result, 0, 9);
                    if (result[0] == 0x01) break ;
                }

                return new double[] { (double)((result[3] * 256 + result[4]) /100.000) , (double)(result[5] * 256 + result[6])};

            }
            catch (Exception)
            {

                return new double[] { -8888, -8888 };
            }
          

          


        
        
        }

        public bool set_voltage_curent(double voltage/*V*/,double current/*MA*/ )
        {


            try
            {
                byte[] result = { 0, 0, 0, 0, 0, 0, 0, 0};
                for (int i = 0; i < 3; i++)
                {
                    for (int cc = 0; cc < 8; cc++) result[cc] = 0;
                    System.Threading.Thread.Sleep(50);
                    byte[] tempcode = { 0x01, 0x10, 0x00, 0x00,  0x00, 0x02, 0x04, (byte)(voltage *100 / 256), (byte)(voltage *100 % 256), (byte)(current / 256), (byte)(current % 256)};
                    byte[] setcode = tan_modbus(tempcode);
                    this.Write(setcode, 0, setcode.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, 8);
                    if (result[0] == 0x01) break;
                }

                return true;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }



        }

        public bool set_ovp_ocp(double voltage, double current)
        {


            try
            {
                byte[] result = { 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(50);
                    byte[] tempcode = { 0x01, 0x10, 0x00, 0x52, 0x00, 0x02, 0x04, (byte)(voltage * 100 / 256), (byte)(voltage * 100 % 256), (byte)(current / 256), (byte)(current % 256) };
                    byte[] setcode = tan_modbus(tempcode);
                    this.Write(setcode, 0, setcode.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, 8);
                    if (result[0] == 0x01) break;
                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }



        }

        public bool set_on_off(byte ison)
        {


            try
            {
                byte[] result = { 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(50);
                    byte[] tempcode = { 0x01, 0x06, 0x00, 0x09, 0x00, ison };
                    byte[] setcode = tan_modbus(tempcode);
                    this.Write(setcode, 0, setcode.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, 8);
                    if (result[0] == 0x01) break;
                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }



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

      
        // crc校验函数
        private   UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~DPS3005()
        {
            this.Close();
        }

    }

}

