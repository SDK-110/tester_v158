using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class HP_2016_ThrustMeter : SerialPort
    {
        string recebuf;
      

        public HP_2016_ThrustMeter(string port, int baudrate=9600) : base(port)
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
            set_to_N();
            //Sendcomm__Setup_sigle();
            set_zero_max(); 
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
        
        public float Sendcomm_Read_data_sigle(Int16 fistaddr=1, Int16 funcode=3,Int16 startAddr=0, Int16 ReadNum=2) {

            byte[] m = {(Byte)(fistaddr),(Byte)funcode, (Byte)(startAddr >> 8), (Byte)(((Byte)startAddr << 8) >> 8), (Byte)(ReadNum >> 8) ,(Byte)(((Byte)ReadNum << 8) >> 8) };
            byte[] senddata = tan_modbus(m);
            byte[] resu = { 0,0,0,0,0,0XFF,0xff,0,0};


            try
            {
                for (int retry = 0; retry < 3; retry++)
                {
                    this.ReadExisting();
                    this.Write(senddata, 0, senddata.Length);
                    resu = new byte[4 + 1 + ReadNum * 2];
                    System.Threading.Thread.Sleep(50);
                    this.Read(resu, 0, resu.Length);
                    if (resu[0] == fistaddr && resu[1] == funcode) break;
                }
            }
            catch {
                resu = new byte[] { 0,0,0,0xff,0xff,0XFF,0xff,0,0};
            }



      return    BitConverter.ToSingle(new byte[]{resu[6],resu[5],resu[4],resu[3] }, 0);
        }



        public void set_to_N(Int16 fistaddr = 1, Int16 funcode = 3, Int16 startAddr = 0X0B, Int16 ReadNum = 2)
        {

        for(int i = 0; i < 5; i++) { 
            byte[] m = { (Byte)(fistaddr), (Byte)funcode, (Byte)(startAddr >> 8), (Byte)(((Byte)startAddr << 8) >> 8), (Byte)(ReadNum >> 8), (Byte)(((Byte)ReadNum << 8) >> 8) };
            byte[] senddata = tan_modbus(m);
            byte[] resu = { 0, 0, 0, 0, 0, 0XFF, 0xff, 0, 0 };


            try
            {
                for (int retry = 0; retry < 3; retry++)
                {
                    this.ReadExisting();
                    this.Write(senddata, 0, senddata.Length);
                    resu = new byte[4 + 1 + ReadNum * 2];
                    System.Threading.Thread.Sleep(50);
                    this.Read(resu, 0, resu.Length);
                    if (resu[0] == fistaddr && resu[1] == funcode && resu[4]==0) return;
                }
            }
            catch
            {
                resu = new byte[] { 0, 0, 0, 0xff, 0xff, 0XFF, 0xff, 0, 0 };
            }
                Sendcomm__Setup_sigle(0x01, 0x06, 0x64, 0X04);
                System.Threading.Thread.Sleep(50);
              
               
            }

            System.Windows.Forms.MessageBox.Show("力度计通讯不当，请检查"); 
        }

        public void Sendcomm__Setup_sigle(Int16 fistaddr=1, Int16 funcode=0x06, Int16 startAddr=0x64, Int16 WriteData=8)
        {

            byte[] m = { (Byte)(fistaddr), (Byte)funcode,  (Byte)(startAddr >> 8), (Byte)(((Byte)startAddr << 8) >> 8), (Byte)(WriteData >> 8),(Byte)(((Byte)WriteData << 8) >> 8), };
            byte[] senddata = tan_modbus(m);
            this.Write(senddata, 0, senddata.Length);

        }

        public void set_zero_max() {



            Sendcomm__Setup_sigle(0x01, 0x06, 0x64, 0X08);

        }


        //public void Sendcomm_Write_write_data(Int16 fistaddr, Int16 regN, byte[] data)
        //{

        //    byte[] m = { 0x1, 0x10,(Byte)(fistaddr >> 8), (Byte)(((Byte)fistaddr << 8) >> 8),
        //                  (Byte)(regN >> 8),(Byte)(((Byte)regN << 8) >> 8),
        //                 (Byte)(regN*2)

        //                 };
        //    byte[] m_add = m.Concat(data).ToArray();
        //    byte[] write_ret_data = new byte[8];
        //    int ct = 0;
        //    do
        //    {
        //        byte[] senddata = tan_modbus(m_add);
        //        this.Write(senddata, 0, senddata.Length);
        //        ct++;
        //        System.Threading.Thread.Sleep(300);
        //        this.Read(write_ret_data, 0, write_ret_data.Length);

        //    } while (write_ret_data[1] != 0x10 && ct < 3);

        //}
        //public byte[] Read_buff_frame(Int16 fistaddr, Int16 regN) {
        //    byte[] temp = new byte[4 + regN * 2];
        //    int ct = 0;
        //    for (int i = 0; i < temp.Length; i++) {
        //        temp[i] = 0;
        //    }
        //    do
        //    {
        //        Sendcomm_Read_buff_frame(fistaddr, regN);
        //        System.Threading.Thread.Sleep(300);
        //        this.Read(temp, 0, temp.Length);
        //        ct++;
        //    } while (temp[1] != 0x03 && ct < 3);


        //    return temp;
        //}

        public void Write_frame() {




        }


        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~HP_2016_ThrustMeter() { 
            this.Close();
        }

    }

}

