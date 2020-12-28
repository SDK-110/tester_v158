using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace testapp
{

    struct rq
    {

        public string index;
        public byte[] record;
        public int recindex;
        public DateTime dt;
    }




    public static class RFID_reader
    {
        #region // RFID api 

        public const byte NEEDSERIAL = 0x08;//仅对指定UID号的卡操作
        public const byte NEEDHALT = 0x20;//读卡或写卡后顺便休眠该卡，休眠后，卡必须拿离开感应区，再放回感应区，才能进行第二次操作。

        //外部函数声明：让设备发出声响
        [DllImport("OUR_MIFARE.dll", EntryPoint = "pcdbeep", CallingConvention = CallingConvention.StdCall)]
        static extern byte pcdbeep(UInt32 xms);//xms单位为毫秒 
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //读取设备编号，可做为软件加密狗用,也可以根据此编号在公司网站上查询保修期限
        [DllImport("OUR_MIFARE.dll", EntryPoint = "pcdgetdevicenumber", CallingConvention = CallingConvention.StdCall)]
        static extern byte pcdgetdevicenumber(byte[] devicenumber);//devicenumber用于返回编号 

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //轻松读卡
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693readex", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693readex(byte ctrlword, byte afi, byte startblock, byte blocknum, byte[] uid, byte[] revbuf);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //afi：应用领域识别号,只能操作相同识别号的卡,一般卡出厂时AFI为0
        //startblock：起始块号
        //blocknum：本次操作的块数量
        //uid：卡序列号,15693卡序列号为8个字节
        //revbuf：用于返回卡块内信息,最大49个字节,其中头一个字节存放返回的字节数,紧跟着的为卡块内信息.
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        //轻松写卡
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693writeex", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693writeex(byte ctrlword, byte afi, byte startblock, byte blocknum, byte[] uid, byte[] writebuf);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //afi：应用领域识别号,只能操作相同识别号的卡,一般卡出厂时AFI为0
        //startblock：起始块号
        //blocknum：本次操作的块数量
        //uid：卡序列号,15693卡序列号为8个字节
        //writebuf：用于指定写入的卡块内信息,最大49个字节,其中头一个字节存放本次要写入的字节数,紧跟着的为卡块内信息.

        //寻一张卡------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693inventory", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693inventory(byte flags, byte afi, byte masklen, byte[] maskuid, byte[] revuid);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //afi：应用领域识别号,只能操作相同识别号的卡,一般卡出厂时AFI为0
        //masklen：掩码bit位数，一般置为0
        //maskuid：卡序列号掩码,为8个字节，指的是卡号和maskuid中的masklen位数值相同的卡片才能寻得到
        //revuid：返回DSFID及卡序列号,第0字节为DSFID，第1到8为卡序列号，共为8个字节
        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        //让卡片进入闲置（Quiet）状态
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693stayquiet", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693stayquiet(byte ctrlword, byte[] uid);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //afi：应用领域识别号,只能操作相同识别号的卡,一般卡出厂时AFI为0
        //寻多张卡--------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693inventory16", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693inventory16(byte flags, byte afi, byte masklen, byte[] maskuid, byte[] revlen, byte[] revuid);
        //参数说明
        //flags：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //afi：应用领域识别号,只能操作相同识别号的卡,一般卡出厂时AFI为0
        //masklen：掩码bit位数，一般置为0
        //maskuid：卡序列号掩码,为8个字节，指的是卡号和maskuid中的masklen位数值相同的卡片才能寻得到
        //revlen:返回长度
        //revuid：返回DSFID及卡序列号,第0字节为DSFID，第1到8为卡序列号，共为8个字节

        //读块数据---------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693readblock", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693readblock(byte ctrlword, byte startblock, byte blocknum, byte[] uid, byte[] revlen, byte[] revbuf);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //startblock：起始块号
        //blocknum：本次操作的块数量
        //uid：卡序列号,15693卡序列号为8个字节
        //revlen:返回长度
        //revbuf：用于返回卡块内信息,最大255个字节,其中头一个字节存放返回的字节数,紧跟着的为卡块内信息.

        //块写数据---------------------------------------------------------------------------------------------------------------------------------------------------
        [DllImport("OUR_MIFARE.dll", EntryPoint = "iso15693writeblock", CallingConvention = CallingConvention.StdCall)]
        static extern byte iso15693writeblock(byte ctrlword, byte startblock, byte blocknum, byte[] uid, int revlen, byte[] revbuf);
        //参数说明
        //ctrlword：控制字,用常量NEEDSERIAL,NEEDHALT赋值,这两常量说明请看声明后的注释
        //startblock：起始块号
        //blocknum：本次操作的块数量
        //uid：卡序列号,15693卡序列号为8个字节
        //revlen:返回长度
        //revbuf：写卡块内信息,最大255个字节，紧跟着的为卡块内信息.
        #endregion //结束API
        static Dictionary<string, rq> re = new Dictionary<string, rq>();


        public static void beep()
        {

            pcdbeep(50);
        }
        public static byte[] read_rfid(byte startblock, byte blocknum, out string stauscode)
        {
            byte afi = 0x00;
            byte status;
            byte ctrlword = 0;
            byte[] uidbuf = new byte[8];
            byte[] revbuf = new byte[200];

            status = iso15693readex(ctrlword, afi, startblock, blocknum, uidbuf, revbuf);

            switch (status)
            {
                case 0:
                    stauscode = "0";
                    int cout = revbuf[0];
                    byte[] rt = new byte[cout];
                    for (int i = 0; i < cout; i++)
                    {
                        rt[i] = revbuf[1 + i];

                    }
                    return rt;


                case 8:
                    stauscode = "not_find_card";
                    return new byte[] { 0 };

                case 22:
                    stauscode = "read_error";
                    return new byte[] { 0 };

                case 23:
                    stauscode = "driver_not_find";
                    return new byte[] { 0 };

                default:
                    stauscode = "unknow_error";
                    return new byte[] { 0 };



            }

        }
        public static bool write_rfid_block(byte startblock, byte[] writedata, out string stauscode)
        {
            byte status;//存放返回值
            byte afi = 0;
            byte[] uidbuf = new byte[8];
            byte[] writebuf = new byte[50];//卡数据缓冲
            writebuf[0] = 4;
            for (int i = 0; i < writedata.Length; i++)
            {

                writebuf[1 + i] = writedata[i];
            }
            byte ctrlword = 0;
            status = iso15693writeex(ctrlword, afi, startblock, 1, uidbuf, writebuf);

            switch (status)
            {
                case 0:
                    stauscode = "0";
                    return true;

                case 8:
                    stauscode = "not_find_card";
                    return false;

                case 22:
                    stauscode = "read_error";
                    return false;

                case 23:
                    stauscode = "driver_not_find";
                    return false;

                default:
                    stauscode = "unknow_error";
                    return false;

            }

        }
        public static byte[] datetime2byte(DateTime tm)
        {


            string year = Convert.ToString(int.Parse(tm.Year.ToString().Substring(2, 2)), 2).PadLeft(7, '0');

            string month = Convert.ToString(tm.Month, 2).PadLeft(4, '0');
            string day = Convert.ToString(tm.Day, 2).PadLeft(5, '0');
            string hour = Convert.ToString(tm.Hour, 2).PadLeft(5, '0');
            string minute = Convert.ToString(tm.Minute, 2).PadLeft(6, '0');


            UInt32 m = Convert.ToUInt32(year + month + day + "000" + hour + "00" + minute, 2);

            byte[] p = System.BitConverter.GetBytes(m);



            return p;

        }
        public static DateTime byte2datetime(byte[] mum)
        {

            UInt32 tm = System.BitConverter.ToUInt32(mum, 0);

            string m = Convert.ToString(tm, 2).PadLeft(32, '0');
         //   System.Windows.Forms.MessageBox.Show(m);
            int year = 2000 + Convert.ToInt16(m.Substring(0, 7), 2);
            int month = Convert.ToInt16(m.Substring(7, 4), 2);
            int day = Convert.ToInt16(m.Substring(11, 5), 2);
            int hour = Convert.ToInt16(m.Substring(19, 5), 2);
            int minute = Convert.ToInt16(m.Substring(26, 6), 2);
            return new DateTime(year, month, day, hour, minute, 0);

        }

        public static void clearrecord()
        {

            re.Clear();
        }

        public static void test1()
        {

            re.Add("indices", new rq() { recindex = 1 });


        }

        public static string write_electronic( UInt32 elenum,byte block =24)
        {

           
                string s = "";
            write_rfid_block(block, BitConverter.GetBytes(elenum), out s);
                if (s == "0")
                {


                    return "pass";
                }

            return "fail";
        }

        public static string write_bare_pcb( UInt32 elenum, byte block = 25/*0x0067*/)
        {

         
                string s = "";
                write_rfid_block(block, BitConverter.GetBytes(elenum), out s);
                if (s == "0")
                {
                 
                    return "pass";
                }

                return "fail";
    

        }

        public static string write_assembled_pcb(UInt32 elenum, byte block =26)
        {

       
                string s = "";
                write_rfid_block(block, BitConverter.GetBytes(elenum), out s);
                if (s == "0")
                {
                   
                    return "pass";
                }
                else
                {

                    return "fail";
                }
          

        }



        public static string write_schematic(UInt32 elenum, byte block = 27)
        {


            string s = "";
            write_rfid_block(block, BitConverter.GetBytes(elenum), out s);
            if (s == "0")
            {

                return "pass";
            }
            else
            {

                return "fail";
            }


        }



        public static string write_indices(byte[] sr, byte block = 29)
        {
         
                string s = "";
                write_rfid_block(block, sr, out s);
                if (s == "0")
                {
                    return "pass";
                }
                else
            { 
                    return "fail";
                }
       

        }





    

        public static string write_date(DateTime dt, byte block =34)
        {

            string st;
                write_rfid_block(block, datetime2byte(dt), out st);
                if (st == "0")
                {
                
                    return "pass";
                }
                else
                {

                    return "fail";
                }
          

       

        }

        public static string write_manfacturerid(string bt, byte block = 31)
        {
            string st = "";
            byte[]  getbyte = System.Text.Encoding.ASCII.GetBytes(bt);
            write_rfid_block(block, getbyte, out st);
            if (st != "0") return "fail";

            return "pass";
        }


        // read

        public static UInt32 read_electronic(byte block = 24)
        {

        
                string s = "";
             byte[]    rs = read_rfid(block, 1, out s);
                if (s == "0") { return BitConverter.ToUInt32(rs, 0); }
                else
                {

                    return 0;
                }
          
          




        }

        

        public static UInt32 read_bare_pcb( byte block = 25)
        {

          
                string s = "";
              byte[]  rs = read_rfid(block, 1, out s);
                if (s == "0") { return  BitConverter.ToUInt32(rs,0); }
                else
                {

                    return 0;
                }
            
         

        }

     public static UInt32 read_assembled_pcb(byte block = 26)
        {

        
                string s = "";
              byte[]  rs = read_rfid(block, 1, out s);
        if (s == "0") { return BitConverter.ToUInt32(rs, 0); }
        else
        {

            return 0;
        }


    }
        public static UInt32 read_schematic( Byte block =27)
        {

           
                string s = "";
             byte []   rs = read_rfid(block, 1, out s);
                if (s == "0") { return BitConverter.ToUInt32(rs, 0); }
                else
                {

                    return 0;
                }
        

        }
        public static byte[] read_indices(byte block = 29)
        {
         
                string s = "";
              byte[]  rs = read_rfid(block, 1, out s);
                if (s == "0") { return rs; }
                else
                {

                    return new byte[4];
                }
        
        }
        public static DateTime read_date( byte block = 34)
        {

        
                string st = "";
                byte []  rs = read_rfid(block, 1, out st);
                if (st == "0")
                {
                  
                    return  byte2datetime(rs);
                }
                else
                {

                    return  new DateTime(2000,1,1,1,1,1);
                }
           
        }

        public static string read_manfacturerid(byte block = 31)
        {
            string st = "";
           byte[] rs = read_rfid(block, 1, out st);
            if (st == "0") {

                return Encoding.UTF8.GetString(rs);
            }

            return "fail";
        }

        public static UInt32 read_softnum(byte block = 18)
        {
            string st = "";
          byte []  rs = read_rfid(block, 1, out st);
            if (st == "0") return BitConverter.ToUInt32(rs,0); 
        
            return 0;
        }

        public static byte[] read_softdrawindex( byte block = 19)
        {

            
            string st = "";
          byte[]  rs = read_rfid(block, 1, out st);

            if (st == "0") return rs;

            return new byte[4];
        }


    }
}








