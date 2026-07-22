using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace testapp
{

    enum status_test {
        test_nodef,
        test_fail,
        Test_mode_didnot_start,
        motor_speed0_to_7_zero_crossing_All_passed,
        motor_speed_off_failed,
        motor_speed1_failed,
        motor_speed2_failed,
        motor_speed3_failed,
        motor_speed4_failed,
        motor_speed5_failed,
        motor_speed6_failed,
        motor_speed7_failed,
        zero_crossing_failed
    }
    class RevAir_com : SerialPort
    {
        volatile int loopwaitflog = 0;
        string version = "-1";
        int titletime = -1;
        string resetttime = "ng";
        int Temperature = -100;
        string gear_duty = "ng";
        int Zero_Crossing = -100;
        string Temp_Limit_complete = "ng";
        int[] motor_level = new int[] { -1, -1, -1, -1, -1, -1, -1 };
        int[] Motor_Temp_Limit = new int[] { -1, -1 };
        string test_status = "ng";
        string recebuf;
        status_test stud = status_test.test_nodef;
        private void setinit() {
            version = "-1";
            titletime = -1;
            resetttime = "ng";
            Temperature = -100;
            gear_duty = "ng";
            Zero_Crossing = -100;
            motor_level = new int[] { -1, -1, -1, -1, -1, -1, -1 };
            Motor_Temp_Limit = new int[] { -1, -1 };
            stud = status_test.test_nodef;
            test_status = "ng";
            Temp_Limit_complete = "ng";
            loopwaitflog = 0;
        }

        void waiting_lp(int count) {

            int ct = count;
            do
            {
                System.Threading.Thread.Sleep(50);

            } while (loopwaitflog == 0 && ct-- >= 0);
        
        }
        public RevAir_com(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
             base.DataReceived += comm_DataReceived;
          
 
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
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



        public int intotestmode() {

            setinit();
            byte[] senddata = new byte[]
                {
                82,
                65,
                3,
                19,
                27,
                85
            };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);
                return (int)stud;

            }
            catch
            {

                return -1;
            }





        }

        public string ReadVersion() {
            setinit();
            byte[] senddata = new byte[] { 0X52, 0X41, 0X03, 0X10, 0X13, 0X55 };
          
            try {
               
                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return version;

            }
            catch {

                return "-1";
            }


        }

        public string reset_motor_time() {
            setinit();
            byte[] senddata = new byte[] {
                82,
                65,
                3,
                18,
                17,
                85
            };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);
                return resetttime ;

            }
            catch
            {

                return "ng";
            }



        }
        public int temperature() {

            setinit();
            byte[] senddata = new byte[]
                {
                82,
                65,
                3,
                19,
                27,
                85
            };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return Temperature;

            }
            catch
            {

                return -10;
            }

        }

        public string set_temperature_limit(int hi_limit, int low_limit)
        {
            setinit();
            int num = 20 ^ low_limit ^ hi_limit;

            byte[] senddata = new byte[]
                {
                    82,
                    65,
                    5,
                    22,
                    (byte)low_limit,
                    (byte)hi_limit,
                    (byte)num,
                    85
            };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return Temp_Limit_complete;

            }
            catch
            {

                return "ng";
            }

        }

        public int [] read_MotorLevel_And_Temp_Limit()
        {

            setinit();
            byte[] senddata = new byte[]
              {
                82,
                65,
                3,
                23,
                22,
                85
            };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return new int[] {
                motor_level[0], motor_level[1],
                motor_level[2],motor_level[3],
                motor_level[4],motor_level[5],
                motor_level[6],
                Motor_Temp_Limit[0] ,Motor_Temp_Limit[1]};

            }
            catch
            {

                return new int[] {-1,-1,-1,-1,-1,-1,-1,-1,-1 };
            }

        }

        public int get_Zero_crossing() {


            setinit();

            byte[] senddata = new byte[]
             {
                0x52,
                0x41,
                0x3,
                0x15,
                0x1b,
                0x55
           };
            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return Zero_Crossing;

            }
            catch
            {

                return -100;
            }



        }

        public int[] save_set_motor(int v0,int v1, int v2, int v3, int v4, int v5, int v6) {
            setinit();
            int mum = 20 ^ v0^ v1 ^ v2 ^v3 ^v4^v5 ^ v6;

            byte[] senddata = new byte[]
                {
                    82,
                    65,
                    10,
                    20,
                    (byte)v0,
                     (byte)v1,
                     (byte)v2,
                     (byte)v3,
                     (byte)v4,
                     (byte)v5,
                     (byte)v6,
                    (byte)mum,
                    85
                };

            try
            {

                this.DiscardInBuffer();
                this.ReadExisting();
                this.Write(senddata, 0, senddata.Length);
                waiting_lp(20);

                return read_MotorLevel_And_Temp_Limit();

            }
            catch
            {

                return new int[]{-1,-1, -1, -1, -1, -1 - 1, -1,-1 };
            }



        }




       

        #region 知识储备库忽略
        /*
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
                // crc校验函数
                private   UInt16 crc16(Byte[] ptr)
                { return ModbusCrc16.Compute(ptr); }

                private  Byte[] tan_modbus(Byte[] data)
                { return ModbusCrc16.AppendCrc(data); }
        */
        #endregion
        ~RevAir_com()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                setinit();
                System.Threading.Thread.Sleep(20);
                var rever = ((SerialPort)sender);
                int bytesToRead = rever.BytesToRead;
              //  int readBufferSize = rever.ReadBufferSize;
                byte[] array = new byte[bytesToRead];
                int[] array2 = new int[7];
                int[] array3 = new int[9];
                rever.Read(array, 0, bytesToRead);
                if (bytesToRead > 5 && array[0] == 82 && array[1] == 65)
                {

                    loopwaitflog = 1;
                    switch (array[3])
                    {
                        case 16:
                            {

                                version = string.Concat(new object[] { array[4], ".", array[5] });
                                return;
                            }
                        case 17:
                            {

                                titletime = ((int)array[4] << 16) + ((int)array[5] << 8) + (int)array[6];
                                return;
                            }
                        case 18:
                            {
                                resetttime = "ok";
                                return;
                            }
                        case 19:
                            {

                                Temperature = array[4];

                                return;
                            }
                        case 20:
                            {
                                gear_duty = "ok";
                                return;
                            }
                        case 21:
                            {
                                if (array[4] > 47 && array[4] < 53)
                                {
                                    array[4] = 50;
                                }
                                else if (array[4] > 57 && array[4] < 63)
                                {
                                    array[4] = 60;
                                }
                                else
                                {
                                    array[4] = 0;
                                }
                                if (array[4] != 0)
                                {

                                    Zero_Crossing = array[4];


                                }
                                return;
                            }
                        case 22:
                            {
                                Temp_Limit_complete = "ok";
                                return;
                            }
                        case 23:
                            {
                                for (int i = 0; i < 7; i++)
                                {
                                    array2[i] = (int)(200 - array[i + 4]);
                                }
                                motor_level = array2;
                                Motor_Temp_Limit[0] = array[11];
                                Motor_Temp_Limit[1] = array[12];

                                return;
                            }
                        case 24:
                            array3[0] = (int)(array[4] & 1);
                            array3[1] = (int)(array[4] & 2);
                            array3[2] = (int)(array[4] & 4);
                            array3[3] = (int)(array[4] & 8);
                            array3[4] = (int)(array[4] & 16);
                            array3[5] = (int)(array[4] & 32);
                            array3[6] = (int)(array[4] & 64);
                            array3[7] = (int)(array[4] & 128);
                            array3[8] = (int)(array[5] & 1);
                            if (array[4] == 0)
                            {
                                stud = status_test.Test_mode_didnot_start;
                                return;
                            }
                            if (array[4] == 255 && array[5] == 1)
                            {


                                stud = status_test.motor_speed0_to_7_zero_crossing_All_passed;
                                return;
                            }
                            if (array3[0] == 0)
                            {
                                stud = status_test.motor_speed_off_failed;

                            }
                            if (array3[1] == 0)
                            {
                                stud = status_test.motor_speed1_failed;

                            }
                            if (array3[2] == 0)
                            {
                                stud = status_test.motor_speed2_failed;

                            }
                            if (array3[3] == 0)
                            {
                                stud = status_test.motor_speed3_failed;

                            }
                            if (array3[4] == 0)
                            {
                                stud = status_test.motor_speed4_failed;
                            }
                            if (array3[5] == 0)
                            {
                                stud = status_test.motor_speed5_failed;
                            }
                            if (array3[6] == 0)
                            {
                                stud = status_test.motor_speed6_failed;
                            }
                            if (array3[7] == 0)
                            {
                                stud = status_test.motor_speed7_failed;
                            }
                            if (array3[8] == 0)
                            {
                                stud = status_test.zero_crossing_failed;
                            }
                            break;
                        default:
                            return;
                    }
                }
            }
            catch( Exception a)
            {
               // System.Windows.Forms.MessageBox.Show("Test");

            }
        }

    }

}

