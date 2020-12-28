using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwBluetoothSig;
using RohdeSchwarz.RsCmwGprfGen;
using RohdeSchwarz.RsInstrument;
namespace testapp.mylib
{
    class cmw100_SpectrumAnalyzers
    {
       private  RsInstrument specan  = null;
        public cmw100_SpectrumAnalyzers(String resource  = "TCPIP0::192.168.8.89::inst0::INSTR")
        {

            try // Separate try-catch for initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                // Initialization:
                //-----------------------------------------------------------
                specan = new RsInstrument(resource);
                specan.VisaTimeout = 3000; // Timeout for VISA Read Operations
                specan.OpcTimeout = 15000; // Timeout for opc-synchronised operations
                specan.InstrumentStatusChecking = true; // Error check after each command
            }
            catch (RsInstrumentException e)
            {

                System.Windows.Forms.MessageBox.Show("instrument load error");


            }

           
        }

        public bool init_SpectrumAnalyzers(double centerFreq=2.412 /*GHz*/,
                                           double reflevel = 10/*dbm*/,
                                            double span=200/*MHz*/, 
                                            double RBW = 200/*KHz*/,
                                            double VBW = 300/*KHz*/,
                                            double sweep = 10001/*sweep points*/)
        {
            try
            {
                specan.WriteString("*RST;*CLS"); // Reset the instrument, clear the Error queue
                delay(100);
                specan.WriteString("INIT:CONT OFF"); // Switch OFF the continuous sweep
                delay(100);
                specan.WriteString("SYST:DISP:UPD ON"); // Display update ON - switch OFF after debugging
                delay(100);
                specan.WriteString($"FREQ:CENT {centerFreq}GHz"); // Setting the center frequency
                delay(100);

                specan.WriteString($"DISP:WIND:TRAC:Y:RLEV {reflevel}"); // Setting the Reference Level
                delay(100);
                specan.WriteString($"FREQ:SPAN {span}MHz"); // Setting the span
                delay(100);
                specan.WriteString($"BAND {RBW}kHz"); // Setting the RBW
                delay(100);
                specan.WriteString($"BAND:VID {VBW}kHz"); // Setting the VBW
                delay(100);
                specan.WriteString($"SWE:POIN {sweep}"); // Setting the sweep points
                delay(100);
                specan.QueryOpc(); // Using *OPC? query waits until all the instrument settings are finished
                return true;
              
            }
            catch (RsInstrumentException e) {

              //  System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }

        }

        public bool getmark_feq_level(out double freq , out double level) {
            freq = 0;
            level = 0;
            try
            {

                specan.VisaTimeout = 2000; // Sweep timeout - set it higher than the instrument acquisition time
                specan.WriteString("INIT"); // Start the sweep
                specan.QueryOpc(); // Using *OPC? query waits until the instrument finished the acquisition
                specan.Binary.FloatNumbersFormat = RohdeSchwarz.RsInstrument.InstrBinaryFloatNumbersFormat.Single4Bytes;
                double[] traceBin = specan.Binary.QueryBinOrAsciiFloatArray("FORM REAL,32;:TRAC? TRACE1"); // Query ascii or binary data
                specan.WriteString("CALC1:MARK1:MAX"); // Set the marker to the maximum point of the entire trace
                specan.QueryOpc(); // Using *OPC? query waits until the marker is set
                var markerX = specan.QueryDouble("CALC1:MARK1:X?");
                var markerY = specan.QueryDouble("CALC1:MARK1:Y?");
                freq = markerX;
                level = markerY;

            }
            catch {

                freq = -10000;
                level = -10000;

                return false;

            }




            return true;
        }

        public void debug_screencapture() {

            specan.WriteString("HCOP:DEV:LANG PNG");
            specan.WriteString(@"MMEM:NAME 'c:\temp\Dev_Screenshot.png'");
            specan.WriteString("HCOP:IMM"); // Make the screenshot now
            specan.QueryOpc(); // Wait for the screenshot to be saved
            specan.File.FromInstrumentToPc(@"c:\temp\Dev_Screenshot.png", @"PC_Screenshot.png"); // Query the instrument file
          


        }

        void delay(int ms) {


            System.Threading.Thread.Sleep(ms);


        }


        ~cmw100_SpectrumAnalyzers() {



            specan.Dispose();

        }

    }

    
}
