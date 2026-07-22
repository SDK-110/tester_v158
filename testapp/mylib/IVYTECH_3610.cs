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
  public  class IVYTHCH_3610 : SerialPort
    {


        string recebuf;
        public IVYTHCH_3610(string port, int baudrate=57600) : base(port)
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
            try_comm();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region /*废弃*/
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
        #endregion



        public void try_comm() {

            try
            {
                this.WriteLine("SYST:RME");
                string m = "";
                this.ReadExisting();
                for (int i = 0; i < 3; i++)
                {

                    System.Threading.Thread.Sleep(20);
                    this.WriteLine("*IDN?");
                    System.Threading.Thread.Sleep(30);
                    m = this.ReadLine();

                }

            }
            catch {



                System.Windows.Forms.MessageBox.Show("IVYTECH DC POWER supply error ");


            }




        }

        public void set_vol_cur(double vol, double curr) {
         //   set_on_off(0);
            System.Threading.Thread.Sleep(10);
            this.WriteLine(String.Format("VOLT {0}",vol));
            System.Threading.Thread.Sleep(10);
            this.WriteLine(String.Format("CURR {0}", curr));
            System.Threading.Thread.Sleep(10);
           // set_on_off(1);
        }

        public void set_on_off(int on_off_state) {

            if (on_off_state != 0)
            {

                this.WriteLine("OUTP 1");
            }
            else {

                this.WriteLine("OUTP 0");

            }
        
        
        }

        public double[] get_vol_cur() {

            try
            {
                double[] dbrsu = { -1, -1 }; 
                for (int c = 1; c < 3; c++)
                {
                    this.WriteLine("MEAS:VCM?");
                    string[] result = this.ReadLine().Trim().Split(",".ToArray());
                    if (result.Length == 3) { dbrsu[0] = double.Parse(result[0]); dbrsu[1] = double.Parse(result[1]); break; }
                }


                return dbrsu;
            }
            catch {

                return new double[] { -0, 001, -0, 00001 };

            }


          
        }



        public int set_vol_step(double target_vol, int steptime, int step)
        {
            try
            {
                for (int tryc = 0; tryc < 3; tryc++)
                {

                    System.Threading.Thread.Sleep(10);
                    this.WriteLine("MEAS:VCM?");
                    string[] result = this.ReadLine().Trim().Split(",".ToArray());
                    if (result.Length != 2) continue;

                    double readvol = double.Parse(result[0]);
                    if (Math.Abs(readvol - target_vol) <= 0.01)
                    {

                        return 1;
                    }
                    else {

                            double step_delta = Math.Abs(readvol - target_vol) / (step * 1.00000);
                            int delay_t = steptime / step;

                            if (readvol - target_vol > 0)
                            {
                                for(int stepc = 0; stepc < step; stepc++)
                                {

                                    this.WriteLine(String.Format("VOLT {0}", readvol - step_delta*step));
                                    System.Threading.Thread.Sleep(delay_t);
                                }
                            

                            }
                            else {

                                for (int stepc = 0; stepc < step; stepc++)
                                {

                                    this.WriteLine(String.Format("VOLT {0}", readvol + step_delta * step));
                                    System.Threading.Thread.Sleep(delay_t);
                                }


                            }

                        return 1;
                    }


                }
                return -1;
            }
            catch {




                return -1;

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

        /// <summary>
        /// Car refrigerator power-on test (defaults: 10.1V standby / 11.0V start / 0.3V wire loss)
        /// </summary>
        public bool CarRefrigeratorPowerONTest()
        {
            return CarRefrigeratorPowerONTest(10.1, 10, 0.15, 11.0, 60, 3.0, 0.3);
        }

        /// <summary>
        /// Car refrigerator power-on test with explicit parameters.
        /// Phase 1: standby at device_standby_v, monitor standby_sec seconds, must stay under standby_limit.
        /// Phase 2: jump to device_start_v, wait start_sec seconds for current > start_threshold.
        /// Power supply voltage = device voltage + wire_loss.
        /// </summary>
        public bool CarRefrigeratorPowerONTest(
            double device_standby_v, int standby_sec, double standby_limit,
            double device_start_v, int start_sec, double start_threshold,
            double wire_loss)
        {
            double supply_standby_v = device_standby_v + wire_loss;
            double supply_start_v = device_start_v + wire_loss;

            try
            {
                // Phase 1: Standby current @ device_standby_v
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase1: set {supply_standby_v:F2}V (device {device_standby_v:F2}V), check standby current...");

                set_on_off(0);
                System.Threading.Thread.Sleep(100);
                set_vol_cur(supply_standby_v, 10.0);
                System.Threading.Thread.Sleep(200);
                set_on_off(1);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < standby_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double[] values = get_vol_cur();
                    if (values.Length < 2 || values[1] < 0)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = values[1];
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_standby_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > standby_limit)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: standby overcurrent {cur:F3}A > {standby_limit}A");
                        set_on_off(0);
                        return false;
                    }
                }

                mylib.utility_func.callbackdebuginfo("[FridgeTest] Phase1 PASS");

                // Phase 2: Compressor start at device_start_v
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2: jump to {supply_start_v:F2}V (device {device_start_v:F2}V), detect compressor start...");
                set_on_off(0);
                set_vol_cur(supply_start_v, 10.0);
                System.Threading.Thread.Sleep(500);
                set_on_off(1);

                for (int i = 0; i < start_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double[] values = get_vol_cur();
                    if (values.Length < 2 || values[1] < 0)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = values[1];
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_start_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > start_threshold)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: compressor start {cur:F3}A > {start_threshold}A");
                        set_on_off(0);
                        return true;
                    }
                }

                mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: no start current > {start_threshold}A within {start_sec}s");
                set_on_off(0);
                return false;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo("[FridgeTest] exception: " + ex.Message);
                try { set_on_off(0); } catch { }
                return false;
            }
        }

        /// <summary>
        /// Car refrigerator low-voltage shutdown test (defaults: 11.7V start / 10.1V hold / 9.4V shutdown / 0.3V wire loss)
        /// </summary>
        public bool CarRefrigeratorPowerOFFTest()
        {
            return CarRefrigeratorPowerOFFTest(11.7, 20, 4.0, 10.1, 5, 3.0, 9.4, 5, 0.15, 0.3);
        }

        /// <summary>
        /// Car refrigerator low-voltage shutdown test with explicit parameters.
        /// Phase 1: start at device_start_v, wait start_timeout seconds for current > start_threshold.
        /// Phase 2: hold at device_hold_v, observe hold_sec seconds, every reading must be > hold_threshold.
        /// Phase 3: drop to device_shutdown_v, observe shutdown_sec seconds, current must drop below shutdown_limit.
        /// Power supply voltage = device voltage + wire_loss.
        /// </summary>
        public bool CarRefrigeratorPowerOFFTest(
            double device_start_v, int start_timeout, double start_threshold,
            double device_hold_v, int hold_sec, double hold_threshold,
            double device_shutdown_v, int shutdown_sec, double shutdown_limit,
            double wire_loss)
        {
            double supply_start_v = device_start_v + wire_loss;
            double supply_hold_v = device_hold_v + wire_loss;
            double supply_shutdown_v = device_shutdown_v + wire_loss;

            try
            {
                // Phase 1: Power-on, wait for compressor start
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase1: set {supply_start_v:F2}V (device {device_start_v:F2}V), wait for compressor start (>{start_threshold}A)...");

                set_on_off(0);
                System.Threading.Thread.Sleep(100);
                set_vol_cur(supply_start_v, 10.0);
                System.Threading.Thread.Sleep(200);
                set_on_off(1);

                bool started = false;
                for (int i = 0; i < start_timeout; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double[] values = get_vol_cur();
                    if (values.Length < 2 || values[1] < 0)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = values[1];
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_start_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > start_threshold)
                    {
                        mylib.utility_func.callbackdebuginfo("[FridgeTest] Phase1 PASS: compressor started");
                        started = true;
                        break;
                    }
                }

                if (!started)
                {
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: compressor not started within {start_timeout}s");
                    set_on_off(0);
                    return false;
                }

                // Phase 2: Hold voltage, fridge must keep running
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2: drop to {supply_hold_v:F2}V (device {device_hold_v:F2}V), fridge must keep running (>{hold_threshold}A)...");

                set_vol_cur(supply_hold_v, 10.0);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < hold_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double[] values = get_vol_cur();
                    if (values.Length < 2 || values[1] < 0)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = values[1];
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_hold_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur <= hold_threshold)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: fridge shut down at {device_hold_v:F2}V (current {cur:F3}A <= {hold_threshold}A)");
                        set_on_off(0);
                        return false;
                    }
                }

                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2 PASS: fridge keeps running at {device_hold_v:F2}V");

                // Phase 3: Drop to shutdown voltage, fridge must stop
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase3: drop to {supply_shutdown_v:F2}V (device {device_shutdown_v:F2}V), wait for shutdown (<{shutdown_limit}A)...");

                set_vol_cur(supply_shutdown_v, 10.0);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < shutdown_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double[] values = get_vol_cur();
                    if (values.Length < 2 || values[1] < 0)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = values[1];
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_shutdown_v:F2}V - sec {i + 1}: {cur:F3}A");
                }

                double[] finalVals = get_vol_cur();
                double finalCur = finalVals[1];
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_shutdown_v:F2}V - final: {finalCur:F3}A");

                if (finalCur < shutdown_limit)
                {
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: low-voltage shutdown, {finalCur:F3}A < {shutdown_limit}A");
                    set_on_off(0);
                    return true;
                }
                else
                {
                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: fridge still running, {finalCur:F3}A >= {shutdown_limit}A");
                    set_on_off(0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo("[FridgeTest] exception: " + ex.Message);
                try { set_on_off(0); } catch { }
                return false;
            }
        }
        ~IVYTHCH_3610()
        {
            this.Close();
        }

    }

}

