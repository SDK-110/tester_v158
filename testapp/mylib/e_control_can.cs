using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using can_j1939_test;
namespace testapp
{


    class e_control_can : SerialPort
    {
        byte[] result = new byte[17];
        volatile int revflog = 0;
        private void setinit() {
        result = new byte[17]; ;
            revflog = 0;
        }
        public e_control_can(string port, int baudrate=9600) : base(port)
        {

            base.NewLine = "\r\n";
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
           //
          
 
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();

            for(int i = 0; i < 3; i++) { 
            base.Write(new byte[] {(byte)'+', (byte)'+',(byte)'+' },0,3);
            System.Threading.Thread.Sleep(50);
            string m = base.ReadExisting();
                if (m.Length <= 0) continue;
            base.WriteLine("ATO");
            System.Threading.Thread.Sleep(50);
            m = base.ReadExisting();
                if (m.Length >= 2) break;
                if (i == 2) { System.Windows.Forms.MessageBox.Show("USB CAN maybe Has been damaged, Please try Restart the software or  replace it");}
            }
              base.DataReceived += comm_DataReceived;


        }



        void loopdelay(int delaynum) {



                int i = 0;
                while (revflog == 0 && i< delaynum) {

                    System.Threading.Thread.Sleep(100);
                 if(i++==(delaynum-2)) throw new Exception("timeout!");
            }

 
        
        
        }



        public void sendcommand_and_return_data(uint PGN,byte[] data ,ref byte[] result,int datelen=8) {
            setinit();
            byte[] commandbyte = new byte[17];
            commandbyte[0] = 0xaa;
            commandbyte[1] = 0x01;
            commandbyte[2] = 0x00;
            commandbyte[3] = (byte)datelen;
            commandbyte[16] = 0x7a;
            uint id = id_pgn.pgn2id(PGN, 6, 0, 242);
            byte[] idv = BitConverter.GetBytes(id);
            commandbyte[4] = idv[3];
            commandbyte[5] = idv[2];
            commandbyte[6] = idv[1];
            commandbyte[7] = idv[0];
            for (int i = 0; i < data.Length; i++) {
                commandbyte[8 + i] = data[i];

            }
            try
            {

                for (int i = 0; i < 3; i++)
                {
                    this.Write(commandbyte, 0, 17);
                    loopdelay(10);
                    if (result[0] != 0XAA && result[16] == 0x7a && result[1] == 0x01 && result[2] == 0x00 &&
                        result[4]==idv[3] && result[5] == idv[2] && result[6] == idv[1] && result[7] == idv[0]
                        ) continue;
                    else break;

                }

                for (int i = 0; i < 8; i++)
                {
                    result[i] = result[8+i];

                }

            }
            catch
            {


                result = new byte[8];

              
            }





        }





        public  byte[] get_testcase_value(byte [] testcase)
        {
            setinit();
            try
            {

                for (int i = 0; i < 3; i++) {
                this.Write(testcase, 0, 17);
                loopdelay(10);
                    if (result[0] != 0XAA && result[16] == 0x7a && result[1]==0x01 && result[2]==0x00) continue;
                    else break;

                }
                return result;
            }
            catch {


                result = new byte[17];

                return result;
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
        ~e_control_can()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                setinit();
                System.Threading.Thread.Sleep(50);
                var rever = ((SerialPort)sender);
                int bytesToRead = rever.BytesToRead;
                this.Read(result, 0, 17);
                revflog = 1;
                
            }
            catch
            {


            }
        }

    }

}

