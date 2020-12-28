// C# Example for RTO / RTE / RTP Oscilloscopes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RohdeSchwarz.RsInstrument; // .NET component providing all the necessary VISA extended functionalities

namespace RsInstrument_RTO2000_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RsInstrument scope;
            try // Separate try-catch for initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                // Initialization:
                //-----------------------------------------------------------
                // Adjust the VISA Resource string to fit your instrument
                scope = new RsInstrument("TCPIP::10.212.1.131::INSTR");
                scope.VisaTimeout = 3000; // Timeout for VISA Read Operations
                scope.OpcTimeout = 15000; // Timeout for opc-synchronised operations
                scope.InstrumentStatusChecking = true; // Error check after each command
            }
            catch (RsInstrumentException e)
            {
                Console.WriteLine("Error initializing the scope session:\n{0}", e.Message);
                Console.WriteLine("Press any key to finish.");
                Console.ReadKey();
                return;
            }

            try // try block to catch any InstrumentErrorException()
            {
                Console.WriteLine("RsInstrument Driver Version: {0}, Core Version: {1}", scope.Identification.DriverVersion, scope.Identification.CoreVersion);
                scope.ClearStatus(); //Clear instrument status - errors and io buffers
                Console.WriteLine("Instrument Identification string:\n{0}", scope.Identification.IdnString);
                scope.WriteString("*RST;*CLS"); // Reset the instrument, clear the Error queue
                scope.WriteString("SYST:DISP:UPD ON"); // Display update ON - switch OFF after debugging
                //-----------------------------------------------------------
                // Basic Settings:
                //-----------------------------------------------------------
                scope.WriteString("ACQ:POIN:AUTO RECL"); // Define Horizontal scale by number of points
                scope.WriteString("TIM:RANG 0.01"); // 10ms Acquisition time
                scope.WriteString("ACQ:POIN 20002"); // 20002 X points
                scope.WriteString("CHAN1:RANG 2"); // Horizontal range 2V
                scope.WriteString("CHAN1:POS 0"); // Offset 0
                scope.WriteString("CHAN1:COUP AC"); // Coupling AC 1MOhm
                scope.WriteString("CHAN1:STAT ON"); // Switch Channel 1 ON
                //-----------------------------------------------------------
                // Trigger Settings:
                //-----------------------------------------------------------
                scope.WriteString("TRIG1:MODE AUTO"); // Trigger Auto mode in case of no signal is applied
                scope.WriteString("TRIG1:SOUR CHAN1"); // Trigger source CH1
                scope.WriteString("TRIG1:TYPE EDGE;:TRIG1:EDGE:SLOP POS"); // Trigger type Edge Positive
                scope.WriteString("TRIG1:LEV1 0.04"); // Trigger level 40mV
                scope.QueryOpc(); // Using *OPC? query waits until all the instrument settings are finished
                // -----------------------------------------------------------
                // SyncPoint 'SettingsApplied' - all the settings were applied
                // -----------------------------------------------------------
                // Arming the SCOPE for single acquisition
                // -----------------------------------------------------------
                scope.VisaTimeout = 2000; // Acquisition timeout - set it higher than the acquisition time
                scope.WriteString("SING");
                // -----------------------------------------------------------
                // DUT_Generate_Signal() - in our case we use Probe compensation signal
                // where the trigger event (positive edge) is reoccuring
                // -----------------------------------------------------------
                scope.QueryOpc(); // Using *OPC? query waits until the instrument finished the Acquisition
                // -----------------------------------------------------------
                // SyncPoint 'AcquisitionFinished' - the results are ready
                // -----------------------------------------------------------
                // Fetching the waveform in ASCII format
                // -----------------------------------------------------------
                double[] waveformAsc = scope.Binary.QueryBinOrAsciiFloatArray("FORM ASC;:CHAN1:DATA?"); // Query ascii or binary data
                Console.WriteLine("Instrument returned {0} samples in the waveformASC array", waveformAsc.Length);
                // -----------------------------------------------------------
                // Fetching the trace in Binary format
                // Transfer of traces in binary format is faster.
                // The waveformBIN data and waveformASC data are however the same.
                // -----------------------------------------------------------
                scope.Binary.FloatNumbersFormat = InstrBinaryFloatNumbersFormat.Single4Bytes;
                double[] waveformBin = scope.Binary.QueryBinOrAsciiFloatArray("FORM REAL,32;:CHAN1:DATA?"); // Query ascii or binary data
                Console.WriteLine("Instrument returned {0} samples in the waveformBIN array", waveformBin.Length);
                // -----------------------------------------------------------
                // Making an instrument screenshot and transferring the file to the PC
                // -----------------------------------------------------------
                scope.WriteString("HCOP:DEV:LANG PNG"); // Set the screenshot format
                scope.WriteString(@"MMEM:NAME 'c:\temp\Dev_Screenshot.png'"); // Set the screenshot path
                scope.WriteString("HCOP:IMM"); // Make the screenshot now
                scope.QueryOpc(); // Wait for the screenshot to be saved
                scope.File.FromInstrumentToPc(@"c:\temp\Dev_Screenshot.png", @"c:\Temp\PC_Screenshot.png"); // Read the response and store to the file in PC
                Console.WriteLine(@"Screenshot file saved to PC 'c:\Temp\PC_Screenshot.png'");
            }
            catch (RsInstrumentException e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                Console.WriteLine("Press any key to finish.");
                Console.ReadKey();
            }
        }
    }
}