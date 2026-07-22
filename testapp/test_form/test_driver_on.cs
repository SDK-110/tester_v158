using InControls.Common;
using InControls.PLC.FX;
using MathNet.Numerics.Statistics;
using PCHMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.test_form
{
    public partial class test_driver_on : Form
    {
        public static test_driver_on instance = null;
        public static FxSerialDeamon _FxSerial;
        public static int Running_flag = 0;
        FxCommandResponse res;
        private test_driver_on()
        {
            InitializeComponent();
            _FxSerial = new FxSerialDeamon();
            _FxSerial.Start(9);
            Running_flag = 1;
        }

        private void test_driver_on_Load(object sender, EventArgs e)
        {
            this.Hide();



        }

        public static test_driver_on getInstance()
        {

            if (instance == null)
            {

                instance = new test_driver_on();
            }

            return instance;
        }

        public void test()
        {

            string cmd = FxCommandHelper.Make(FxCommandConst.FxCmdRead, new FxAddress("X0", ControllerTypeConst.ctPLC_Fx), 16);
            res = _FxSerial.Send(0, cmd);
            cmd = FxCommandHelper.Make(FxCommandConst.FxCmdRead, new FxAddress("Y0", ControllerTypeConst.ctPLC_Fx), 16);
            res = _FxSerial.Send(0, cmd);

        }

        private void test_driver_on_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        public static void destroy_driver()
        {

            if (_FxSerial != null && Running_flag == 1)
            {

                _FxSerial.Stop();
                Running_flag = 0;
                _FxSerial.Dispose();
            }



        }
    }
}
