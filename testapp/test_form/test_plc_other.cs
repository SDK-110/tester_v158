using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using InControls.Common;
using InControls.PLC.FX;
using InControls.PLC;

namespace testapp.test_form
{
    public partial class test_plc_other : Form
    {
        // SRND_CM_12DI _12DI = new SRND_CM_12DI("COM9");

        int z=0;
        System.Threading.Timer test;
        public test_plc_other()
        {


            InitializeComponent();

            Task.Factory.StartNew(() => {

                // 创建并配置串口
                SerialPort serialPort = new SerialPort("COM9", 19200, Parity.Even, 7, StopBits.One);

                try
                {
                    // 打开串口
                    serialPort.Open();

                    // 发送 0x05
                    byte[] data = new byte[] { 0x05 };
                    serialPort.Write(data, 0, data.Length);
                    System.Threading.Thread.Sleep(100);
                    serialPort.Write(data, 0, data.Length);
                    System.Threading.Thread.Sleep(100);
                    serialPort.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                   
                }
                finally
                {
                    // 关闭串口
                    if (serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }
                }



            });




        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            //  test_2.get_instance().Show();
            testapp.PLC.plc_option.test_ex_module_modbus();
           // testapp.PLC.plc_option.melsec_serial();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            //  Pictureshow.getInstance().show("");

            FxSerialDeamon _FxSerial;
            _FxSerial = new FxSerialDeamon();
            _FxSerial.Start(9, "19200,E,7,1");
           
            String cmd = "";
            FxCommandResponse res;
           cmd = FxCommandHelper.Make(FxCommandConst.FxCmdRead, new FxAddress("X0", ControllerTypeConst.ctPLC_Fx), 16);
            res = _FxSerial.Send(0, cmd);
            if (z++ % 2 == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    cmd = FxCommandHelper.Make(FxCommandConst.FxCmdForceOn, new FxAddress($"Y1{i}", FxAddressLayoutType.AddressLayoutByte));
                    res = _FxSerial.Send(0, cmd);
                }

            }
            else {

                for (int i = 0; i < 8; i++)
                {
                    cmd = FxCommandHelper.Make(FxCommandConst.FxCmdForceOff, new FxAddress($"Y1{i}", FxAddressLayoutType.AddressLayoutByte));
                    res = _FxSerial.Send(0, cmd);
                }


            }
        

            //cmd = FxCommandHelper.Make(FxCommandConst.FxCmdRead, new FxAddress("M0", ControllerTypeConst.ctPLC_Fx), 2);
            //res = _FxSerial.Send(0, cmd);
            //Console.WriteLine(string.Format("成批读 \t{0}", res.ToString()));

            // 针对 M001..M077 设置与读取
            //for (int i = 0; i < 64; i++)
            //{
            //    cmd = FxCommandHelper.Make(FxCommandConst.FxCmdForceOff,
            //                            new FxAddress(string.Format("M{0}", i), FxAddressLayoutType.AddressLayoutByte));
            //    res = _FxSerial.Send(0, cmd);
            //    Console.WriteLine(res.ToString());


            //}


            cmd = FxCommandHelper.Make(FxCommandConst.FxCmdForceOn,
                                    new FxAddress(string.Format("M{0}", 0), FxAddressLayoutType.AddressLayoutByte));
            res = _FxSerial.Send(0, cmd);
            Console.WriteLine(res.ToString());
            _FxSerial.Enabled=false;
            _FxSerial.Dispose();


            //cmd = FxCommandHelper.Make(FxCommandConst.FxCmdRead, new FxAddress("M0", ControllerTypeConst.ctPLC_Fx), 10);
            //res = _FxSerial.Send(0, cmd);
            //var m = res.ResponseValue;

            //List<uint> lst = new List<uint>() { (uint)3 };
            //for (int k = 0; k < 10; k++)
            //    lst.Add((uint)k);

            //cmd = FxCommandHelper.Make<UInt32DataType>(FxCommandConst.FxCmdWrite, new FxAddress("D1", ControllerTypeConst.ctPLC_Fx), lst);
            //res = _FxSerial.Send(0, cmd);

            //for (int i = 0; i < 9; i++)
            //{
            //    cmd = FxCommandHelper.Make(FxCommandConst.FxCmdForceOn,
            //                            new FxAddress(string.Format("Y{0}", Convert.ToString(i, 8)), FxAddressLayoutType.AddressLayoutByte));
            //    res = _FxSerial.Send(0, cmd);
            //}

        }

        private void button3_Click(object sender, EventArgs e)
        {
            HslCommunication.Profinet.Melsec.MelsecFxSerial fx2n;
            fx2n = new HslCommunication.Profinet.Melsec.MelsecFxSerial();
            fx2n.SerialPortInni("com9", 19200,7,StopBits.One, Parity.Even);
            fx2n.Open();
            fx2n.Write("y7", true);
           
            //  test_driver_on.getInstance().test();
        }

        innove_relay rl;
        private void button4_Click(object sender, EventArgs e)
        {
            if(rl==null)rl = new innove_relay();

            var p = new bool[32] ;
            p[7] = true;
            rl.data = p;
            rl.set_relay_kbca132s();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            test_pchmi_instance.get_instance().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mylib.utility_func.ex_module_crc16_str());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            testapp.PLC.modbus_sets.nmodubs4_test();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new test_visa();
        }
    }
}
