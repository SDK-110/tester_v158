using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.mylib
{
    internal class ac_source_tuodapu
    {
        ModbusRtu hslcomm_modubus = new ModbusRtu(1);
        public ac_source_tuodapu(string port)
        {
            hslcomm_modubus.SerialPortInni(port, 9600, 8, System.IO.Ports.StopBits.One, Parity.None);
            hslcomm_modubus.Open();
        }

        public int set_voltage_current(int hl_change,double voltage, double freq, int output) {
            hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;
            var send_data = new UInt16[] { (UInt16)hl_change, (UInt16)(voltage * 10), (UInt16)(freq * 10), (UInt16)(output) };
            utility_func.callbackdebuginfo($"send data: ch:{(UInt16)hl_change};vol:{(UInt16)(voltage)};freq:{(UInt16)(freq)};onoff:{output}");
            var pdata = hslcomm_modubus.Write($"s=1;x={0x10};{0x20}",send_data);
            if (pdata.IsSuccess) {
                utility_func.callbackdebuginfo("setting ok");
                return 1;
            } else { 
            
            return 0; 
            };



        }
        public int get_Freq_vol_cur_power_pf(ref double[] data)
        {
            hslcomm_modubus.ByteTransform.DataFormat = HslCommunication.Core.DataFormat.DCBA;

            var pdata = hslcomm_modubus.Read($"s=1;x={0x03};{0x01}", 5);
            if (pdata.IsSuccess)
            {
                byte[] dts = pdata.Content;
                data[0] = (double)((dts[0] * 256 + dts[1]) / 10.0);
                data[1] = (double)((dts[2] * 256 + dts[3]) / 10.0);
                data[2] = (double)((dts[4] * 256 + dts[5]) / 10.0);
                data[3] = (double)((dts[6] * 256 + dts[7]) / 10.0);
                data[4] = (double)((dts[8] * 256 + dts[9]) / 10.0);
                return 1;
            }
            else
            {

                return 0;
            };



        }
        public void dispose() {

            hslcomm_modubus.Close();

        }

        ~ac_source_tuodapu() {


            hslcomm_modubus.Close();
        
        
        
        }
    }
}
