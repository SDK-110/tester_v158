using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using testapp.mylib;
using Windows.ApplicationModel.Activation;
using testapp;
namespace DeviceLibrary
{
 public   class TH6300 : SerialPort
    {

        public TH6300(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.ReadTimeout = 2000;
            base.RtsEnable = true;
            base.DtrEnable = true;

            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();

         base.WriteLine("OUTPUT OFF");
        }

        private void TH6300_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public void setcurrent( string value)
        {
          
            this.WriteLine("CURR" + " " + value);
        }
        public string getcurrent(string ch)
        {
            this.DiscardInBuffer();
            this.WriteLine("CURR " +  "?");
            System.Threading.Thread.Sleep(30);
            return this.ReadLine();
        }

        public int set_vol_cur(double Voltage,double Current)
        {
            try
            {
                utility_func.callbackdebuginfo($"set voltage:{Voltage},current:{Current}");
                this.WriteLine("APPLy" + " " + Voltage + "," + Current);

                return 1;
            }
            catch {
                return 0;
            }
        }
        public void setvolatage(string Voltage)
        {

            this.WriteLine("VOLT" + " " + Voltage);

        }

        public string getvolatage()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:VOLT"+ "?");
            System.Threading.Thread.Sleep(30);
            string tm = this.ReadLine();
            utility_func.callbackdebuginfo(tm);
            return tm;
        }
        public string getCurrent()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:CURR" + "?");
            System.Threading.Thread.Sleep(30);
            string tm = this.ReadLine();
            utility_func.callbackdebuginfo(tm);
            return tm;
        }

        public int General_command(string set_p)
        {
            try
            {

                this.WriteLine(set_p);

                return 0;
            }
            catch {

                return -1;
            }
        
        }
        public int set_vol_slowly(double target_v, double spantime, int times,int ch=0)
        {
            try
            {
                System.Threading.Thread.Sleep(30);
                string zzz = this.ReadExisting();
                System.Threading.Thread.Sleep(30);
                this.WriteLine("MEA: VOLT" + "?");
                System.Threading.Thread.Sleep(100);
                double rs=-1;
                string m = this.ReadLine().Replace("V","").Replace(@"\n","").Replace(@"\r","");
                if (double.TryParse(m, out rs)==false) return -1;
                if (Math.Abs(rs - target_v) <= 0.01) return 1;
                if (rs >= 0)
                {


                    if ((target_v - rs) > 0)
                    {
                        double z = Math.Abs(target_v - rs) / times;

                        for (int i = 0; i <= times; i++)
                        {
                            System.Threading.Thread.Sleep((int)(spantime / times));
                            setvolatage((rs + (z * i)) + "");
                        }

                    }
                    else
                    {

                        double z = Math.Abs(target_v - rs) / times;
                        for (int i = 0; i <= times; i++)
                        {
                            System.Threading.Thread.Sleep((int)(spantime / times));
                            setvolatage((rs - z * i) + "");

                        }

                    }

                    setvolatage(target_v + "");
                    return 1;
                }


                return -2;

            }
            catch {


                return -3;

            }
        }

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
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase1: set {supply_standby_v:F2}V (device {device_standby_v:F2}V), check standby current...");

                set_on_off(0);
                System.Threading.Thread.Sleep(100);
                set_vol_cur(supply_standby_v, 10.0);
                System.Threading.Thread.Sleep(200);
                set_on_off(1);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < standby_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double value = double.NaN;
                    if (!double.TryParse(getCurrent(), out value))
                    {

                        testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                        continue;
                    }

                    if (value< 0)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = value;
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_standby_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > standby_limit)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: standby overcurrent {cur:F3}A > {standby_limit}A");
                        set_on_off(0);
                        return false;
                    }
                }

                testapp.mylib.utility_func.callbackdebuginfo("[FridgeTest] Phase1 PASS");

                // Phase 2: Compressor start at device_start_v
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2: jump to {supply_start_v:F2}V (device {device_start_v:F2}V), detect compressor start...");
                set_on_off(0);
                set_vol_cur(supply_start_v, 10.0);
                System.Threading.Thread.Sleep(500);
                set_on_off(1);

                for (int i = 0; i < start_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double value= double.NaN;

                    if (!double.TryParse(getCurrent(), out value))
                    {

                        testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                        continue;
                    }
                    if (value < 0)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = value;
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_start_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > start_threshold)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: compressor start {cur:F3}A > {start_threshold}A");
                        set_on_off(0);
                        return true;
                    }
                }

                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: no start current > {start_threshold}A within {start_sec}s");
                set_on_off(0);
                return false;
            }
            catch (Exception ex)
            {
                testapp.mylib.utility_func.callbackdebuginfo("[FridgeTest] exception: " + ex.Message);
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
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase1: set {supply_start_v:F2}V (device {device_start_v:F2}V), wait for compressor start (>{start_threshold}A)...");

                set_on_off(0);
                System.Threading.Thread.Sleep(100);
                set_vol_cur(supply_start_v, 10.0);
                System.Threading.Thread.Sleep(200);
                set_on_off(1);

                bool started = false;
                for (int i = 0; i < start_timeout; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double value = double.NaN;
                    if (!double.TryParse(getCurrent(), out value)) {

                        testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                        continue;
                    }
                   
                    if ( value < 0)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = value;
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_start_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur > start_threshold)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo("[FridgeTest] Phase1 PASS: compressor started");
                        started = true;
                        break;
                    }
                }

                if (!started)
                {
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: compressor not started within {start_timeout}s");
                    set_on_off(0);
                    return false;
                }

                // Phase 2: Hold voltage, fridge must keep running
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2: drop to {supply_hold_v:F2}V (device {device_hold_v:F2}V), fridge must keep running (>{hold_threshold}A)...");

                set_vol_cur(supply_hold_v, 10.0);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < hold_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);

                    double value = double.NaN;
                    if (!double.TryParse(getCurrent(), out value))
                    {

                        testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                        continue;
                    }
                    if (value < 0)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = value;
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_hold_v:F2}V - sec {i + 1}: {cur:F3}A");

                    if (cur <= hold_threshold)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: fridge shut down at {device_hold_v:F2}V (current {cur:F3}A <= {hold_threshold}A)");
                        set_on_off(0);
                        return false;
                    }
                }

                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase2 PASS: fridge keeps running at {device_hold_v:F2}V");

                // Phase 3: Drop to shutdown voltage, fridge must stop
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase3: drop to {supply_shutdown_v:F2}V (device {device_shutdown_v:F2}V), wait for shutdown (<{shutdown_limit}A)...");

                set_vol_cur(supply_shutdown_v, 10.0);
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < shutdown_sec; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    double value = double.NaN;
                    if (!double.TryParse(getCurrent(), out value))
                    {

                        testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                        continue;
                    }
                    if (value < 0)
                    {
                        testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] read fail (sec {i + 1})");
                        set_on_off(0);
                        return false;
                    }

                    double cur = value;
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_shutdown_v:F2}V - sec {i + 1}: {cur:F3}A");
                }

                double finalVal = double.NaN;
                if (!double.TryParse(getCurrent(), out finalVal))
                {

                    testapp.mylib.utility_func.callbackdebuginfo($" Power supper comm error\n");
                    
                }
                double finalCur = finalVal;
                testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] {supply_shutdown_v:F2}V - final: {finalCur:F3}A");

                if (finalCur < shutdown_limit)
                {
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: low-voltage shutdown, {finalCur:F3}A < {shutdown_limit}A");
                    set_on_off(0);
                    return true;
                }
                else
                {
                    testapp.mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: fridge still running, {finalCur:F3}A >= {shutdown_limit}A");
                    set_on_off(0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                testapp.mylib.utility_func.callbackdebuginfo("[FridgeTest] exception: " + ex.Message);
                try { set_on_off(0); } catch { }
                return false;
            }
        }

        public int set_on_off(int state)
        {
            try
            {
                System.Threading.Thread.Sleep(30);
                if (state == 1)
                {
                    this.WriteLine("OUTPUT ON");
                }
                else
                {
                    this.WriteLine("OUTPUT OFF");
                }
                return 1;
            }
            catch {
                return 0;
            }

        }



        ~TH6300()
        {
            this.Close();
        }
    }
}

