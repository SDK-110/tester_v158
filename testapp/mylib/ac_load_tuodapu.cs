using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.mylib
{
    internal class tuodapu_ac_load
    {
        ModbusRtu hslcomm_modubus = new ModbusRtu(1);
        public values read_result= new values();
        public tuodapu_ac_load(string port)
        {
            hslcomm_modubus.SerialPortInni(port, 9600, 8, System.IO.Ports.StopBits.One, Parity.None);
            hslcomm_modubus.Open();
            hslcomm_modubus.ReceiveTimeOut = 1000;
           
        }

        public int get_data (ref float[] result_val) {
            hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;

            HslCommunication.OperateResult <byte[]> rs;
            int ct = 2;
            bool rs_status = false;
            read_result = new values();

            do
            {
                // byte m = 2;
                // rs = hslcomm_modubus.Write($"s=1;{0x11}", send_data);
                rs = hslcomm_modubus.Read($"s=0;x={0x3};{0x4}", 12);

                rs_status = rs.IsSuccess;
                if (ct-- <= 0) break;
            } while (rs_status == false);

            if (rs_status)
            {
                byte[] dts = rs.Content;
                float[] p = hslcomm_modubus.ByteTransform.TransSingle(dts, 0, 6);
                result_val[0]= read_result.rms_v = p[0];
                result_val[1]=read_result.rms_c = p[1];
                result_val[2]=read_result.power = p[2];
                result_val[3]=read_result.power_va = p[3];
                result_val[4]=read_result.pf = p[4];
                utility_func.callbackdebuginfo($"voltageRMA:{p[0]} currentRMA{p[1]} power:{p[2]} power_VA:{p[3]} pf:{p[4]}");
                return 1;

            }
            else {

                utility_func.callbackdebuginfo("error: " + rs.ErrorCode);
                return 0;

            }

            



        }

        public int set_default_par(float load_volue,UInt16 vol_range_selector,UInt16 mode,UInt16 voltage) {

              hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;
            // var send_data = new UInt16[] { (UInt16)cur_range_selector, (UInt16)(vol_range_selector), (UInt16)(mode) };

           
            byte[] byteArray = BitConverter.GetBytes(load_volue);
            var send_data = new UInt16[] { (UInt16)(vol_range_selector), (UInt16)(mode), (UInt16)(byteArray[0]*256+ byteArray[1]), (UInt16)(byteArray[2] * 256 + byteArray[3]) };
            utility_func.callbackdebuginfo($"send data=>load_volue:{load_volue};vol_range_selector:{(UInt16)(vol_range_selector)};mode:{(UInt16)(mode)}");
            HslCommunication.OperateResult rs;
            int ct = 2;
            bool rs_status= false;
          
            do
            {
               // byte m = 2;
             // rs = hslcomm_modubus.Write($"s=1;{0x11}", send_data);
             rs = hslcomm_modubus.Write($"s=0;x={0x10};{0x11}", send_data);
            
                rs_status = rs.IsSuccess;
                if (ct-- <= 0) break;
            } while (rs_status == false);   

            if (rs_status)
            {
                utility_func.callbackdebuginfo("setting default par ok");
                hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;

                int ct2 = 2;
                bool rs_status2 = false;
                HslCommunication.OperateResult rs2;
                do
                {

                    rs2 = hslcomm_modubus.Write($"s=1;x={0x10};{0x17}", voltage);
                    rs_status2 = rs2.IsSuccess;
                    if (ct2-- <= 0) break;
                } while (rs_status2 == false);


                if (rs_status2)
                {

                    utility_func.callbackdebuginfo("setting default voltage ok");
                    return 1;
                }
                else {
                    utility_func.callbackdebuginfo("setting default voltage error:" + rs2.ErrorCode + ":" + rs2.Message);
                    return -1;
                }
               
            }
            utility_func.callbackdebuginfo("setting default par error:" + rs.ErrorCode + ":" + rs.Message);
            return -3;
         

        }



        public int set_onoff( UInt16 mode)
        {

            hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;
            
            UInt16 v = mode;

            HslCommunication.OperateResult rs;
            int ct = 2;
            bool rs_status = false;
            do
            {

                rs = hslcomm_modubus.WriteOneRegister($"s=1;{0x15}", v);
                rs_status = rs.IsSuccess;
                if (ct-- <= 0) break;
            } while (rs_status == false);

            if (rs_status)
            {
                utility_func.callbackdebuginfo("set_onoff ok " + v);
                return 1;
            }
            else
            {
                utility_func.callbackdebuginfo("set_onoff error:" + rs.ErrorCode +":"+ rs.Message);
                return 0;
            };

        }


        public void dispose() {

            hslcomm_modubus.Close();

        }

        ~tuodapu_ac_load() {


            hslcomm_modubus.Close();
        
        
        
        }

        internal class values {
            public float rms_v;
            public float rms_c;
            public float power;
            public float power_va;
            public float pf;
        
        }
    }
}
