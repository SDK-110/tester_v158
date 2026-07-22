using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using Code4Bugs.Utils.IO.Modbus;
using Code4Bugs.Utils.IO;
namespace testapp
{
    class DAM_3158A_DAQ : SerialPort
    {
        string recebuf;
        SerialStream serial;
        int[] typerec = new int[7];
    public DAM_3158A_DAQ(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
            serial = new SerialStream(this);
            serial.ReadTimeout = 200;
            Set_type_range(0, 1);
            Set_type_range(1, 1);
            Set_type_range(2, 1);
            Set_type_range(3, 1);
            Set_type_range(4, 1);
            Set_type_range(5, 1);
            Set_type_range(6, 1);
            Set_type_range(7, 1);

        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region //没有用的函数
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

        public void set_sing_relay(byte relay_num, byte openorclose) {

            Byte[] a = new Byte[] { 0x05, 0x01, 0X00,(byte)(relay_num-1),(byte)(openorclose),0x00 };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {

                    count++;
                    if (count > 3) {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0);
        
        
        }
        public void set_relay(byte relay_1_8, byte relay_9_16)
        {

            Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {
                    count++;
                    if (count > 3)
                    {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0 && count < 3);


        }
        #endregion

        public double[] read_analog_quantity(int chinnel) {

            try
            {
                var revbytes = serial.RequestFunc4(1, 0x0100, 0x0008);
             
                var response = revbytes.ToResponseFunc4().Data;
                return new double[] {( double)(((response[0]*256 + response[1])-32767)/32767.000 *10),
                                     ( double)(((response[2]*256 + response[3])-32767)/32767.000 *10),
                                     ( double)(((response[4]*256 + response[5])-32767)/32767.000 *10),
                                     ( double)(((response[6]*256 + response[7])-32767)/32767.000 *10),
                                     ( double)(((response[8]*256 + response[9])-32767)/32767.000 *10),
                                     ( double)(((response[10]*256 + response[11])-32767)/32767.000 *10),
                                     ( double)(((response[12]*256 + response[13])-32767)/32767.000 *10),
                                     ( double)(((response[14]*256 + response[15])-32767)/32767.000 *10),
                                   };
            }
            catch {
              //  System.Windows.Forms.MessageBox.Show("Test");
                return new double[] { -100, -1,-1,-1,-1,-1,-1,-1};
            }
        }

        /// <summary>
              // 输入类型 范围 最大误差 代码
              // V -10V～+10V ±0.1% FS 0x0009
              // V -5V～+5V ±0.1% FS 0x0008
              // V -1V～+1V ±0.1% FS 0x0006
              // V -500mV～+500mV ±0.1% FS 0x0005
              // V -150mV～+150mV ±0.1% FS 0x0004
              // V 0～10V ±0.1% FS 0x000E
              // V 0～5V ±0.1% FS 0x000D
              // V 1～5V ±0.1% FS 0x0082
              // mA -20mA～20mA ±0.1% FS 0x000A
              // mA 0～20mA ±0.1% FS 0x000B
              // mA 4～20mA ±0.1% FS 0x000C
              // mA 0～22mA ±0.1% FS 0x0080
        /// </summary>
        /// <param name="Voltage"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public int  Set_type_range(int channel, int type )
        { 
            try
            {
                byte[] revbytes;
                if (type > 13) return -3;
                if(channel >7) return -2;
                typerec[channel] = type;
                switch (type) {

                    case 1:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel>7?7:channel), 0x0009);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;

                        }
                        break;
                    case 2:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0008);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 3:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0006);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 4:
                        {
                           
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0005);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 5:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0004);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 6:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x000e);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 7:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x000d);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 8:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0082);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 9:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x000a);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 10:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0082);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 11:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x000b);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 12:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x000c);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                    case 13:
                        {
                            revbytes = serial.RequestFunc6(01, 0x0100 + (channel > 7 ? 7 : channel), 0x0080);
                            if ((revbytes.ToResponseFunc6().DataAddress) != 0x0100 + (channel > 7 ? 7 : channel)) return -1;
                        }
                        break;
                }

             

                    return 1;


            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -2;
            }

        }

           
    

    
        



        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~DAM_3158A_DAQ() { 
            this.Close();
            //serial.Dispose();
        }

    }

}

