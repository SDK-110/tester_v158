using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class AMPD_BMS : SerialPort
    {
        string recebuf;
        public AMPD_BMS(string port, int baudrate=56000) : base(port)
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


        public float[] read_voltage_14(byte sportid=0x01,byte tportid=0x10) {

            
            
            try
            {
                byte[] result = new byte[143];
               
                byte[] send_command = { 0x55, sportid, tportid, 0x05, 0x44, 0x41, 0x54, 0x31, 0x3f, 0x3b, 0x20, 0xaa };

                for (int ir = 0; ir < 3; ir++)
                {
                    this.DiscardInBuffer();
                    this.ReadExisting();
                    for (int cc = 0; cc < result.Length; cc++) result[cc] = 0;

                    System.Threading.Thread.Sleep(100);
                    this.Write(send_command, 0, send_command.Length);
                    System.Threading.Thread.Sleep(300);
                    this.Read(result, 0, result.Length);
                    if (result[0] == 0x55 && result[142] == 0xaa) break;
                }
                int i = 4;
                return new float[] {
                    BitConverter.ToSingle(new byte[] { result[i], result[i + 1], result[i + 2], result[i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[2 * i], result[2 * i + 1], result[2 * i + 2], result[2 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[3 * i], result[3 * i + 1], result[3 * i + 2], result[3 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[4 * i], result[4 * i + 1], result[4 * i + 2], result[4 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[5 * i], result[5 * i + 1], result[5 * i + 2], result[5 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[6 * i], result[6 * i + 1], result[6 * i + 2], result[6 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[7 * i], result[7 * i + 1], result[7 * i + 2], result[7 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[8 * i], result[8 * i + 1], result[8 * i + 2], result[8 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[9 * i], result[9 * i + 1], result[9 * i + 2], result[9 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[10 * i], result[10 * i + 1], result[10 * i + 2], result[10 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[11 * i], result[11 * i + 1], result[11 * i + 2], result[11 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[12 * i], result[12 * i + 1], result[12 * i + 2], result[12 * i + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[13 * i], result[13 * i + 1], result[13 * i + 2], result[13 * i + 3] }, 0) };
            }
            catch (Exception e)
            {
               // System.Windows.Forms.MessageBox.Show(e.ToString());
                return new float[] { -8888, -8888 , -8888, -8888 , -8888, -8888 , -8888,
                                     -8888, -8888 , -8888, -8888 , -8888, -8888 , -8889
                                    };
            }
          

          


        
        
        }

        public float[] read_temperature_4(byte sportid = 0x01, byte tportid = 0x10)
        {

            
            try
            {
                byte[] result = new byte[143];
                byte[] send_command = { 0x55, sportid, tportid, 0x05, 0x44, 0x41, 0x54, 0x31, 0x3f, 0x3b, 0x20, 0xaa };

                for (int ir = 0; ir < 3; ir++)
                {
                    this.DiscardInBuffer();
                    this.ReadExisting();
                    for (int cc = 0; cc < result.Length; cc++) result[cc] = 0;

                    System.Threading.Thread.Sleep(100);
                    this.Write(send_command, 0, send_command.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, result.Length);
                    if (result[0] == 0x055 && result[142] == 0xaa) break;
                }
                int i = 4;
                int offset = 116;
                return new float[] {
                    BitConverter.ToSingle(new byte[] { result[offset + i * 0], result[offset + i*0 + 1], result[offset + i * 0 +  2], result[offset + i * 0 + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[offset + i * 1], result[offset + i*1 + 1], result[offset + i * 1 +  2], result[offset + i * 1 + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[offset + i * 2], result[offset + i*2 + 1], result[offset + i * 2 +  2], result[offset + i * 2 + 3] }, 0),
                    BitConverter.ToSingle(new byte[] { result[offset + i * 3], result[offset + i*3 + 1], result[offset + i * 3 +  2], result[offset + i * 3 + 3] }, 0) };
            }
            catch (Exception)
            {

                return new float[] { -8888, -8888 , -8888, -8888 
                                    };
            }







        }

        public float read_current_4(byte sportid = 0x01, byte tportid = 0x10)
        {

           
            try
            {
                byte[] result = new byte[143];
                byte[] send_command = { 0x55, sportid, tportid, 0x05, 0x44, 0x41, 0x54, 0x31, 0x3f, 0x3b, 0x20, 0xaa };

                for (int ir = 0; ir < 3; ir++)
                {
                    this.DiscardInBuffer();
                    this.ReadExisting();
                    for (int cc = 0; cc < result.Length; cc++) result[cc] = 0;

                    System.Threading.Thread.Sleep(100);
                    this.Write(send_command, 0, send_command.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, result.Length);
                    if (result[0] == 0x055 && result[141] == 0xaa) break;
                }
                int i = 4;
                int offset = 131;
                return BitConverter.ToSingle(new byte[] { result[offset + i * 0], result[offset + i*0 + 1], result[offset + i * 0 +  2], result[offset + i * 0 + 3] }, 0);
            }
            catch (Exception)
            {

                return (float)-8888;
            }







        }

        public int read_cell_chemistry(byte sportid = 0x01, byte tportid = 0x10)
        {

            this.DiscardInBuffer();
            try
            {
                byte[] result = new byte[8];
                byte[] send_command = { 0x55, sportid, tportid, 0x05, 0x43, 0x48, 0x45, 0x4d, 0x3f, 0xa2, 0xe6, 0xaa };

                for (int ir = 0; ir < 3; ir++)
                {
                    for (int cc = 0; cc < result.Length; cc++) result[cc] = 0;

                    System.Threading.Thread.Sleep(100);
                    this.Write(send_command, 0, send_command.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, result.Length);
                    if (result[0] == 0x055 && result[7] == 0xaa) break;
                }
                int i = 4;
                int offset = 131;
                return (result[4]==0x31)?1:2;
            }
            catch (Exception)
            {

                return -1;
            }







        }

        public double read_cell_capacity(byte sportid = 0x01, byte tportid = 0x10)
        {

           
            try
            {
                byte[] result = new byte[16];
                byte[] send_command = { 0x55, sportid, tportid, 0x05, 0x43, 0x41, 0x50, 0x41, 0x3f, 0xfa, 0xf1, 0xaa };

                for (int ir = 0; ir < 3; ir++)
                {
                    this.DiscardInBuffer();
                    this.ReadExisting();
                    for (int cc = 0; cc < result.Length; cc++) result[cc] = 0;

                    System.Threading.Thread.Sleep(100);
                    this.Write(send_command, 0, send_command.Length);
                    System.Threading.Thread.Sleep(400);
                    this.Read(result, 0, result.Length);
                    System.Threading.Thread.Sleep(400);
                    if (result[0] == 0x055 && result[15] == 0xaa) break;
                }
                byte[] m = { result[4], result[5], result[6], result[7], result[8], result[9], result[10], result[11], result[12] };
                string str = System.Text.Encoding.ASCII.GetString(m);
                
                return double.Parse(str);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return -1000;
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

        private  Byte[] tan_modbus(Byte[] data,bool iftr)
        {


            Byte[] z = new Byte[(data.Length + 2)];

            for (int i = 0; i < data.Length; i++)
            {


                z[i] = data[i];
            }

            UInt16 temp = crc16(data);
            z[(data.Length)] = (Byte)(((Byte)temp << 8) >> 8);
            z[(data.Length + 1)] = (Byte)(temp >> 8);

            return z;
        }


        public  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

    

    ~AMPD_BMS()
        {
            this.Close();
        }

    }

}

