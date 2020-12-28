// C# Example for RTB2000 / RTM2000 / RTM3000 / RTA4000 Oscilloscopes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RohdeSchwarz.RsInstrument; // .NET component providing all the necessary VISA extended functionalities

namespace RsInstrument_RTB2000_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RsInstrument rtb;

            try // separate try-catch for initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                // Initialization:
                //-----------------------------------------------------------
                // Adjust the VISA Resource string to fit your instrument
                rtb = new RsInstrument("TCPIP::10.112.1.140::INSTR");
                //rtb = new RsInstrument("USB0::0x0AAD::0x01D6::101457::INSTR");
                rtb.VisaTimeout = 3000; // Timeout for VISA Read Operations
                rtb.OpcTimeout = 15000; // Timeout for opc-synchronised operations
                rtb.InstrumentStatusChecking = true; // Error check after each command
            }
            catch (RsInstrumentException e)
            {
                Console.WriteLine("Error initializing the instrument session:\n{0}", e.Message);
                Console.WriteLine("Press any key to finish.");
                Console.ReadKey();
                return;
            }

            try // try block to catch any RsInstrumentException()
            {
                Console.WriteLine("RsInstrument Driver Version: {0}, Core Version: {1}", rtb.Identification.DriverVersion, rtb.Identification.CoreVersion);
                rtb.ClearStatus(); // Clear instrument status - errors and io buffers
                Console.WriteLine("Instrument Identification string:\n{0}", rtb.Identification.IdnString);
                rtb.WriteString("*RST"); // Reset the instrument
                rtb.QueryOpc(); // Wait for the reset to finish
                //-----------------------------------------------------------
                // Basic Settings:
                //---------------------------- -------------------------------
                rtb.WriteString("TIM:ACQT 0.01"); // 10ms Acquisition time
                rtb.WriteString("CHAN1:RANG 5.0"); // Horizontal range 5V (0.5V/div)
                rtb.WriteString("CHAN1:OFFS 0.0"); // Offset 0
                rtb.WriteString("CHAN1:COUP ACL"); // Coupling AC 1MOhm
                rtb.WriteString("CHAN1:STAT ON"); // Switch Channel 1 ON
                //-----------------------------------------------------------
                // Trigger Settings:
                //-----------------------------------------------------------
                rtb.WriteString("TRIG:A:MODE AUTO"); // Trigger Auto mode in case of no signal is applied
                rtb.WriteString("TRIG:A:TYPE EDGE;:TRIG:A:EDGE:SLOP POS"); // Trigger type Edge Positive
                rtb.WriteString("TRIG:A:SOUR CH1"); // Trigger source CH1
                rtb.WriteString("TRIG:A:LEV1 0.05"); // Trigger level 0.05V
                rtb.QueryOpc(); // Using *OPC? query waits until all the instrument settings are finished
                // -----------------------------------------------------------
                // SyncPoint 'SettingsApplied' - all the settings were applied
                // -----------------------------------------------------------
                // Arming the SCOPE for single acquisition
                // -----------------------------------------------------------
                rtb.VisaTimeout = 2000; // Acquisition timeout - set it higher than the acquisition time
                rtb.WriteString("SING");
                // -----------------------------------------------------------
                // DUT_Generate_Signal() - in our case we use Probe compensation signal
                // where the trigger event (positive edge) is reoccuring
                // -----------------------------------------------------------
                rtb.QueryOpc(); // Using *OPC? query waits until the instrument finished the Acquisition
                // -----------------------------------------------------------
                // SyncPoint 'AcquisitionFinished' - the results are ready
                // -----------------------------------------------------------
                // Fetching the waveform in ASCII format
                // -----------------------------------------------------------
                double[] waveformAsc = rtb.Binary.QueryBinOrAsciiFloatArray("FORM ASC;:CHAN1:DATA?"); // Query ascii or binary data
                Console.WriteLine($"Instrument returned {waveformAsc.Length} samples in the waveformASC array");
                // -----------------------------------------------------------
                // Fetching the trace in Binary format
                // Transfer of traces in binary format is faster.
                // The waveformBIN data and waveformASC data are however the same.
                // -----------------------------------------------------------
                rtb.Binary.FloatNumbersFormat = InstrBinaryFloatNumbersFormat.Single4Bytes;
                double[] waveformBin = rtb.Binary.QueryBinOrAsciiFloatArray("FORM:BORD LSBF;:FORM REAL;:CHAN1:DATA?");
                Console.WriteLine($"Instrument returned {waveformBin.Length} samples in the waveformBIN array");

                // -----------------------------------------------------------
                // Making an instrument screenshot and transferring the file to the PC
                // -----------------------------------------------------------

                rtb.WriteString("MMEM:CDIR '/INT/'"); // Change the directory

                // ignore errors generated by the MMEM:DEL command, the error is generated if the file does not exist
                rtb.InstrumentStatusChecking = false;
                rtb.WriteString("MMEM:DEL 'Dev_Screenshot.png'"); // Delete the file if it already exists, otherwise you get 'Execution error'
                rtb.QueryOpc();
                rtb.ClearStatus();
                rtb.InstrumentStatusChecking = true;

                rtb.WriteString("HCOP:LANG PNG;:MMEM:NAME 'Dev_Screenshot'"); // Hardcopy settings for taking a screenshot - notice no file extention here
                rtb.WriteString("HCOP:IMM"); // Make the screenshot now
                rtb.QueryOpc(); // Wait for the screenshot to be saved
                rtb.File.FromInstrumentToPc("Dev_Screenshot.png", @"c:\Temp\PC_Screenshot.png"); // Query the instrument file
                Console.WriteLine(@"Screenshot file saved to PC 'c:\Temp\PC_Screenshot.png'");
            }
            catch (RsInstrumentException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to finish...");
                Console.ReadKey();
            }
        }
    }
}