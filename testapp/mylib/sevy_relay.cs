using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
  public  class sevy_relay : SerialPort
    {
        /*新科继电器*/
        #region 新科继电器 
        private byte sk_relay_1_8_rec = 0;
        private byte sk_relay_9_16_rec = 0;
        private byte sk_relay_17_24_rec = 0;
        private byte sk_relay_25_32_rec = 0;

        public byte get_sk_relay_1_8_rec
        {
            get
            {

                return sk_relay_1_8_rec;
            }
        }
        public byte get_sk_relay_9_16_rec
        {
            get
            {

                return sk_relay_9_16_rec;
            }
        }

        public byte get_sk_relay_17_24_rec
        {
            get
            {

                return sk_relay_17_24_rec;
            }
        }
        public byte get_sk_relay_25_32_rec
        {
            get
            {

                return sk_relay_25_32_rec;
            }
        }

        #endregion 
        /*新科继电器*/



        private byte  relay_1_8_rec=0;
        private byte relay_9_16_rec=0;
        public byte get_relay_1_8_rec {
            get {

                return relay_1_8_rec;
            }
        }
        public byte get_relay_9_16_rec
        {
            get
            {

                return relay_9_16_rec;
            }
        }
        string recebuf;
        public sevy_relay(string port, int baudrate=9600) : base(port)
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
                    if (count >= 3) {

                       // System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                       // while (true) ;
                      
                       
                    }

                }
            } while (count != 0);
        
        
        }
        public void set_relay(byte relay_1_8, byte relay_9_16)
        {
            if(this.IsOpen==false)
            {
                this.Open();
            }
            relay_1_8_rec = relay_1_8;
            relay_9_16_rec = relay_9_16;
            Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                System.Threading.Thread.Sleep(100);
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {
                    count++;
                    if (count >= 3)
                    {

                      //  System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");

                      //  while (true) ;
                      
                    }

                }
            } while (count != 0 && count < 3);


        }
        // crc校验函数
        private   UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }


        /// <summary>
        /// 信科继电器
        /// </summary>
        /// <param name="sendcmd"></param>
        /// <returns></returns>
        private byte [] ks_crc_sum_tranc(byte[] sendcmd) {

            byte[] trancrsu = new byte[8];
            trancrsu[0] = 0x55;

            for (int i = 0; i < sendcmd.Length; i++) {

                trancrsu[i + 1] = sendcmd[i];
            }
           
          
            int  tmp = 0;

            for(int i=0;i<sendcmd.Length+1; i++) {

                tmp = tmp + trancrsu[i];
            }

            trancrsu[7] = (byte)tmp;

            return trancrsu;


        }
        /// <summary>
        /// 青岛信科电子继电器板
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int  set_relay_kbca132s(byte gp1 ,byte gp2, byte gp3, byte gp4 ) {

            sk_relay_1_8_rec = gp1;
            sk_relay_9_16_rec = gp2;
            sk_relay_17_24_rec = gp3;
            sk_relay_25_32_rec = gp4;

            try
            {
                byte[] tr_cmd = ks_crc_sum_tranc(new byte[] { 0x01, 0x34, (byte)~sk_relay_25_32_rec, (byte)~sk_relay_17_24_rec, (byte)~sk_relay_9_16_rec, (byte)~sk_relay_1_8_rec });
                this.Write(tr_cmd, 0, tr_cmd.Length);
                System.Threading.Thread.Sleep(50);
                tr_cmd = ks_crc_sum_tranc(new byte[] { 0x01, 0x35, sk_relay_25_32_rec, sk_relay_17_24_rec, sk_relay_9_16_rec, sk_relay_1_8_rec });
                this.Write(tr_cmd, 0, tr_cmd.Length);


                return 1;

            }
            catch {

                return -1;
            }
            }

        ~sevy_relay()
        {
            this.Close();
        }

    }

}

