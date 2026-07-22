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

    class sqlr_blue_command_4wpel : SerialPort
    {
        volatile int loopwaitflog = 0;
        volatile int revsize = 0;
        const int databufsize = 100;
        volatile byte[] databuf = new byte[databufsize];
        private void setinit() {
            for (int i = 0; i < databufsize; i++) {
                databuf[i] = (byte)i;
            }
           loopwaitflog = 0;
            revsize = 0;
        }

        void waiting_lp(int count) {

            int ct = count;
            do
            {
                System.Threading.Thread.Sleep(50);

            } while (loopwaitflog == 0 && ct-- >= 0);
        
        }
        public sqlr_blue_command_4wpel(string port, int baudrate=9600) : base(port)
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
           // recebuf = sp.ReadExisting();
        }
    

        public int to_connecte_dut() {
            
            for (int i = 0; i < 5; i++) {
            setinit();
            byte[] send_command_array = { 1, 0, 1, 1, 1, 13, 10 };
            this.Write(send_command_array,0,send_command_array.Length);
            send_command_array = new byte[] { 1, 0, 1, 1, 2, 13, 10 };
            this.Write(send_command_array, 0, send_command_array.Length);
            send_command_array = new byte[] { 1, 0, 1, 1, 3, 13, 10 };
            this.Write(send_command_array, 0, send_command_array.Length);
             waiting_lp(50);
             if (loopwaitflog == 1) {
                    if (databuf[2] == 129) { return 129; }
                   // if (databuf[2] == 130) { return -2; } //130 含义不清楚
                } ;
            }
            return -1;
        }

        /// <summary>
        /// 范围没有定义
        /// </summary>
        /// <returns></returns>
        public int read_betty_from_dut()
        {

            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 3, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 131) {

                        return databuf[4]; 

                    }
                };
            }
            return -1;
        }

        public int read_mac_from_dut(out UInt64 rsu)
        {
            rsu = 0;
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 3, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1 && revsize ==12)
                {
                    byte[] temp = new byte[12]; 
                    for (int lp = 0; lp < 12; lp++) {

                        temp[11 - i] = databuf[i];
                    }
                    rsu = BitConverter.ToUInt64(temp, 0);
                    return 1;
                };
            }
            return -1;
        }

        public int read_rssi_from_dut(out int rsu )
        {
            rsu = 300;
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 3, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 133)
                    {

                        rsu= (databuf[4]>127)?(127-databuf[4]): databuf[4];
                        return 1;
                    }
                };
            }
            return -1;
        }

        public int read_fw_from_dut(out string rsu)
        {
            rsu = "readerror";
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 6, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 134)
                    {
                        byte[] tmp = new byte[databuf.Length];
                        for (int lp = 0; lp < databuf.Length - 4; lp++) {
                            tmp[i] = databuf[i + 4];
                        }
                        rsu = System.Text.Encoding.UTF8.GetString(tmp).Trim();
                        return 1;
                    }
                };
            }
            return -1;
        }

        public int read_luxo_from_dut(out Int16 rsu)
        {
            rsu = Int16.MaxValue;
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 7, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 135)
                    {

                        rsu = BitConverter.ToInt16(databuf, 4);
                        return 1;
                    }
                };
            }
            return -1;
        }
        public int read_temp_from_dut(out Int16 rsu)
        {
            rsu = Int16.MaxValue;
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 8, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 136)
                    {

                        rsu = BitConverter.ToInt16(databuf, 4);
                        return 1;
                    }
                };
            }
            return -1;
        }

        public int read_humi_from_dut(out Int16 rsu)
        {
            rsu = Int16.MaxValue;
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 9, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 137)
                    {

                        rsu = BitConverter.ToInt16(databuf, 4);
                        return 1;
                    }
                };
            }
            return -1;
        }

        public int read_GS_from_dut(out Int16[] rsu)
        {
            rsu = new Int16[] { Int16.MaxValue, Int16.MaxValue, Int16.MaxValue };
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 10, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                waiting_lp(50);
                if (loopwaitflog == 1)
                {

                    if (databuf[2] == 137)
                    {
                        rsu = new Int16[] { BitConverter.ToInt16(databuf, 4), BitConverter.ToInt16(databuf, 6), BitConverter.ToInt16(databuf, 8) };

                        return 1;
                    }
                };
            }
            return -1;
        }

        public int setred_to_dut()
        {
          
            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 11, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                return 1;
                ;
            }
            return -1;
        }

        public int setgreen_to_dut()
        {

            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 12, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                return 1;
                ;
            }
            return -1;
        }

        public int setblue_to_dut()
        {

            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 13, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                return 1;
                ;
            }
            return -1;
        }

        public int setledoff_to_dut()
        {

            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 14, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                return 1;
                ;
            }
            return -1;
        }

        public int setdisconnect_to_dut()
        {

            for (int i = 0; i < 5; i++)
            {
                setinit();
                byte[] send_command_array = { 1, 0, 2, 1, 0, 13, 10 };
                this.Write(send_command_array, 0, send_command_array.Length);
                return 1;
                ;
            }
            return -1;
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
        ~sqlr_blue_command_4wpel()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                setinit();
                System.Threading.Thread.Sleep(100);
                var rever = ((SerialPort)sender);
                int bytesToRead = rever.BytesToRead;
                byte[] array = new byte[bytesToRead];
                revsize = bytesToRead;
                rever.Read(array, 0, bytesToRead);
                
                for (int i = 0; i < BytesToRead; i++) {

                    databuf[i] = array[i];
                }
                loopwaitflog = 1;
               
            }
            catch( Exception a)
            {
               // System.Windows.Forms.MessageBox.Show("Test");

            }
        }

    }

}

