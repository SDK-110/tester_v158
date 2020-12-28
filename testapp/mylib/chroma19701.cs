using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace testapp
{
 public   class chroma19701 : SerialPort
    {

        public chroma19701(string port, int baudrate=9600) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.ReadTimeout = 2000;
            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();
            
            //this.Write(new Byte[] { 0xAA, 0x55, 0x02, 0xF4, 0x00, 0xF6 }, 0, 6);
            //System.Threading.Thread.Sleep(200);
            //if (this.ReadByte()!=170) {

            //    throw new Exception("min current miter error ");
            //}


        }

        private void chroma19701_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }


        public void starttest() {


            this.write_comm(new byte[] { 0x22 });


        }

        public void stoptest() {


            this.write_comm(new byte[] { 0x21 });
          
        }

        public void setparameters(setred_step setter) {

            byte[] teap = setter.getval();
            this.Write(teap, 0, teap.Length);

          //  System.Windows.Forms.MessageBox.Show(this.ReadExisting());
        }

        public void write_comm(byte[] z)
        {
            this.DiscardInBuffer();
            byte[] temp = checksum_send(z);

            this.Write(temp, 0, temp.Length);

        }

        public  byte[] checksum_send(byte[] m)
        {

            byte[] sendbyte = new byte[5 + m.Length];

            for (int i = 0; i < sendbyte.Length; i++)
            {

                sendbyte[i] = 0;
            }

            byte p = 0;
            sendbyte[0] = 0xab;
            sendbyte[1] = 0x01;
            sendbyte[2] = 0x70;
            sendbyte[3] = (byte)m.Length;
            for (int i = 0; i < m.Length; i++)
            {
                sendbyte[4 + i] = m[i];
            }
            for (int i = 1; i < sendbyte.Length - 1; i++)
            {


                p = (byte)(p + sendbyte[i]);

            }

            p = (byte)(~p + 1);

            sendbyte[sendbyte.Length - 1] = p;


            return sendbyte;

        }
        public void preset_param(byte ac_freq,byte softagc,byte WvAutoRange, byte IRAutoRange,byte GFI,byte FAILrestart,byte screen) {

            byte[] temp = new byte[] { 0x25,ac_freq, softagc, WvAutoRange, IRAutoRange, GFI, FAILrestart, screen };

            this.write_comm(temp);

        }

        public class  setred_step {

           private byte[] temp = new byte[34];

            public setred_step(byte [] ini)
            {
                Array.Copy(ini, this.temp, this.temp.Length);
            }
            public setred_step() {

                for (int i = 0; i < this.temp.Length; i++) {

                    temp[i] = 0;

                }

            }

            public byte[] getval() {

                byte p = 0;
               
                for (int i = 1; i < this.temp.Length - 1; i++)
                {


                    p = (byte)(p + this.temp[i]);

                }

                p = (byte)(~p + 1);

                this.temp[this.temp.Length - 1] = p;


                return this.temp;

            }
          public byte head
            {
                get { return temp[0]; }
                set { temp[0] = value; }
            }
          public byte target_add
            {
                get { return temp[1]; }
                set { temp[1] = value; }

            }
            public byte source_add
            {
                get { return temp[2]; }
                set { temp[2] = value; }

            }
            public byte datalen
            {
                get { return temp[3]; }
                set { temp[3] = value; }

            }

            public byte comm
            {
                get { return temp[4]; }
                set { temp[4] = value; }

            }
            public byte step
            {
                get { return temp[5]; }
                 set   { temp[5] = value; }

            }

            public byte mod
            {
                get { return temp[6]; }
                set { temp[6] = value; }

            }

            public Int32 voltage
            {
                get { return temp[8]*256 + temp[7]; }
                set {
                    temp[8] = (byte)(value >> 8);
                    temp[7] = (byte)(value);
                }

            }
            public Int32 RampTimems
            {
                get { return ((temp[10] * 256 + temp[9])) * 100 ; }
                set
                {
                    if (value < 100) value = 100;
                    temp[10] = (byte)((value / 100) >> 8);
                    temp[9] = (byte)(value / 100 );
                }

            }

            public Int32 Reserved
            {
                get { return temp[12] * 256 + temp[11]; }
                set
                {
                    temp[12] = (byte)(value >> 8);
                    temp[11] = (byte)(value);
                }

            }
            public Int32 testtimems
            {
                get { return (temp[14] * 256 + temp[13])*100; }
                set
                {
                    if (value < 100) value = 100;
                    temp[14] = (byte)((value / 100) >> 8);
                    temp[13] = (byte)((value / 100));
                }

            }

            public Int32 fallms
            {
                get { return (temp[16] * 256 + temp[15]) * 100; }
                set
                {
                    if (value < 100) value = 100;
                    temp[16] = (byte)((value / 100) >> 8);
                    temp[15] = (byte)((value / 100));
                }

            }

            public float hilimitmA
            {
                get { return   ((float)(temp[20]<<24) + (float)(temp[19]<<16) + (float)(temp[18]<<8) + temp[17])/(float)10.000 ; }
                set
                {

                    if (value < 0.01) value = (float)0.01;
                    temp[20] = (byte)((Int32)(value * 10000) >> 24);
                    temp[19] = (byte)((((Int32)(value * 10000))<<8)>> 24);
                    temp[18] = (byte)((((Int32)(value * 10000))<<16)>> 24);
                    temp[17] = (byte)((((Int32)(value * 10000))<<24)>>24);

                }

            }

            public float lowlimitmA
            {
                get { return ((float)(temp[24] << 24) + (float)(temp[23] << 16) + (float)(temp[22] << 8) + temp[21]) / (float)10.000; }
                set
                {

                    if (value < 0.01) value = (float)0.01;
                    temp[24] = (byte)((Int32)(value * 10000) >> 24);
                    temp[23] = (byte)((((Int32)(value * 10000)) << 8) >> 24);
                    temp[22] = (byte)((((Int32)(value * 10000)) << 16) >> 24);
                    temp[21] = (byte)((((Int32)(value * 10000)) << 24) >> 24);

                }

            }

            public float arclimitmA
            {
                get { return ((float)(temp[28] << 24) + (float)(temp[27] << 16) + (float)(temp[26] << 8) + temp[25]) / (float)10.000; }
                set
                {

                    if (value < 0.01) value = (float)0.01;
                    temp[28] = (byte)((Int32)(value * 10000) >> 24);
                    temp[27] = (byte)((((Int32)(value * 10000)) << 8) >> 24);
                    temp[26] = (byte)((((Int32)(value * 10000)) << 16) >> 24);
                    temp[25] = (byte)((((Int32)(value * 10000)) << 24) >> 24);

                }

            }

            public byte[] reserved
            {
                get {
                    byte[] gt= new byte[] { 0, 0, 0, 0 };
                    gt[0] = temp[29];
                    gt[1] = temp[30];
                    gt[2] = temp[31];
                    gt[3] = temp[32];
                    return gt;
                }
                set
                {

                    temp[29] = value[0];
                    temp[30] = value[1];
                    temp[31] = value[2];
                    temp[32] = value[3];

                }

            }

            public byte chek
            {
                get { return temp[33]; }
                set { temp[33] = value; }
            }
        }
        public class test_result
        {

            private byte[] temp = new byte[29];

            public test_result(byte[] ini)
            {
                Array.Copy(ini, this.temp, this.temp.Length);
            }
            public test_result()
            {

                for (int i = 0; i < this.temp.Length; i++)
                {

                    temp[i] = 0;

                }

            }

           
            public byte head
            {
                get { return temp[0]; }
           
            }
            public byte target_add
            {
                get { return temp[1]; }
             

            }
            public byte source_add
            {
                get { return temp[2]; }
            

            }
            public byte datalen
            {
                get { return temp[3]; }
                

            }

            public byte comm
            {
                get { return temp[4]; }
            

            }
            public byte t_result
            {
                get { return temp[5]; }
            

            }

            public byte step
            {
                get { return temp[6]; }
           

            }

            public byte result_code { 

                get { return temp[7]; }
           

            }
            public byte item_num
            {

                get { return temp[8]; }
           

            }
            public byte test_mode
            {

                get { return temp[9]; }
             

            }

            public Int32 meter1
            {
                get { return temp[11] * 256 + temp[10]; }
            

            }

            public float meter2
            {
                get { return ((float)(temp[15] << 24) + (float)(temp[14] << 16) + (float)(temp[13] << 8) + temp[12]) / (float)10.000; } //微安
             

            }

            public float meter3
            {
                get { return ((float)(temp[19] << 24) + (float)(temp[18] << 16) + (float)(temp[17] << 8) + temp[16]) / (float)10.000; }


            }

            public Int32 item16
            {
                get { return ((temp[21] * 256 + temp[20])) * 100; }
             
            }

          
            public Int32 item32
            {
                get { return (temp[23] * 256 + temp[22]) * 100; }
             

            }

            public Int32 item64
            {
                get { return (temp[21] * 256 + temp[20]) * 100; }
           

            }

            public Int32 item128
            {
                get { return (temp[23] * 256 + temp[22]) * 100; }


            }


            public byte chek
            {
                get { return temp[24]; }
             
            }
        }
        public void store_memory(int mem_num, char [] m) {

            byte[] temp = new byte[2 + m.Length]; 
            for(int i =0;i<m.Length; i++) {

                temp[2 + i] = (byte)m[i];
            }


            temp[0] = 0x26;
            temp[1] = (byte)mem_num;

         //   byte[] send = checksum_send(temp);
            this.write_comm(temp);
            if (this.ReadByte() != 0xab) { System.Windows.Forms.MessageBox.Show("存储失败"); }
        }

        public void recll_memory(int mem_num) {


            byte[] temp = new byte[] { 0x27, (byte)((mem_num > 60) ? 1 : mem_num) };

            this.write_comm(temp);

        }
        public void init_all_parame() {

            byte[] temp = new byte[] { 0x2c};

            this.write_comm(temp);

        }

        public void Remote_Loacl(int localorRemmote)
        {

            byte[] temp = new byte[] { 0x2E, (byte)localorRemmote};

            this.write_comm(temp);

            if (this.ReadByte() != 0xab) { System.Windows.Forms.MessageBox.Show("控制失败"); }

        }

        public test_result getResult(int step) {

            byte[] temp = new byte[] { 0xb1, (byte)step, 0Xff };
         //   byte[] bbbb = this.checksum_send(temp);
            this.write_comm(temp);

            byte[] ret = new byte[34];
            try
            {
                this.Read(ret, 0, ret.Length);
            }
            catch (Exception) {

                System.Windows.Forms.MessageBox.Show("数据传输超时，请反馈技术人员");
            }
            test_result m = new test_result(ret);

            return m;
        }


        ~chroma19701()
        {
            this.Close();
        }
    }
}

