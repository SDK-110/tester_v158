// RsInstrument Specan Example for FSW / FSV / FPS / FSWP / FSQ Spectrum Analyzers

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RohdeSchwarz.RsInstrument; // .NET component providing all the necessary VISA extended functionalities

namespace RsInstrument_FSW_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RsInstrument specan;

            try // Separate try-catch for initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                // Initialization:
                //-----------------------------------------------------------
                
                // Adjust the VISA Resource string to fit your instrument
                specan = new RsInstrument("TCPIP::10.112.1.116::INSTR");
                specan.VisaTimeout = 3000; // Timeout for VISA Read Operations
                specan.OpcTimeout = 15000; // Timeout for opc-synchronised operations
                specan.InstrumentStatusChecking = true; // Error check after each command
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
                Console.WriteLine("RsInstrument Driver Version: {0}, Core Version: {1}", specan.Identification.DriverVersion, specan.Identification.CoreVersion);
                specan.ClearStatus(); // Clear instrument io buffers
                Console.WriteLine("Instrument Identification string:\n{0}", specan.Identification.IdnString);
                specan.WriteString("*RST;*CLS"); // Reset the instrument, clear the Error queue
                specan.WriteString("INIT:CONT OFF"); // Switch OFF the continuous sweep
                specan.WriteString("SYST:DISP:UPD ON"); // Display update ON - switch OFF after debugging
                //-----------------------------------------------------------
                // Basic Settings:
                //-----------------------------------------------------------
                specan.WriteString("DISP:WIND:TRAC:Y:RLEV 10.0"); // Setting the Reference Level
                specan.WriteString("FREQ:CENT 3.0 GHz"); // Setting the center frequency
                specan.WriteString("FREQ:SPAN 200 MHz"); // Setting the span
                specan.WriteString("BAND 100 kHz"); // Setting the RBW
                specan.WriteString("BAND:VID 300kHz"); // Setting the VBW
                specan.WriteString("SWE:POIN 10001"); // Setting the sweep points
                specan.QueryOpc(); // Using *OPC? query waits until all the instrument settings are finished
                // -----------------------------------------------------------
                // SyncPoint 'SettingsApplied' - all the settings were applied
                // -----------------------------------------------------------
                specan.VisaTimeout = 2000; // Sweep timeout - set it higher than the instrument acquisition time
                specan.WriteString("INIT"); // Start the sweep
                specan.QueryOpc(); // Using *OPC? query waits until the instrument finished the acquisition
                // -----------------------------------------------------------
                // SyncPoint 'AcquisitionFinished' - the results are ready
                // -----------------------------------------------------------
                // Fetching the trace in ASCII format
                // -----------------------------------------------------------
                double[] traceAsc = specan.Binary.QueryBinOrAsciiFloatArray("FORM ASC;:TRAC? TRACE1"); // Query ascii or binary data
                Console.WriteLine("Instrument returned {0} samples in the traceAsc array", traceAsc.Length);
                // -----------------------------------------------------------
                // Fetching the trace in Binary format
                // The transfer time of traces in binary format is shorter.
                // The traceBIN data and traceASC data are however the same.
                // -----------------------------------------------------------
                specan.Binary.FloatNumbersFormat = InstrBinaryFloatNumbersFormat.Single4Bytes;
                double[] traceBin = specan.Binary.QueryBinOrAsciiFloatArray("FORM REAL,32;:TRAC? TRACE1"); // Query ascii or binary data
                Console.WriteLine("Instrument returned {0} samples in the traceBin array", traceBin.Length);
                // -----------------------------------------------------------
                // Setting the marker to max and querying the X and Y
                // -----------------------------------------------------------
                specan.WriteString("CALC1:MARK1:MAX"); // Set the marker to the maximum point of the entire trace
                specan.QueryOpc(); // Using *OPC? query waits until the marker is set
                var markerX = specan.QueryDouble("CALC1:MARK1:X?");
                var markerY = specan.QueryDouble("CALC1:MARK1:Y?");
                Console.WriteLine("Marker Frequency {0:F3} Hz, Level {1:F2} dBm", markerX, markerY);
                // -----------------------------------------------------------
                // Making an instrument screenshot and transferring the file to the PC
                // -----------------------------------------------------------
                specan.WriteString("HCOP:DEV:LANG PNG");
                specan.WriteString(@"MMEM:NAME 'c:\temp\Dev_Screenshot.png'");
                specan.WriteString("HCOP:IMM"); // Make the screenshot now
                specan.QueryOpc(); // Wait for the screenshot to be saved
                specan.File.FromInstrumentToPc(@"c:\temp\Dev_Screenshot.png", @"c:\Temp\PC_Screenshot.png"); // Query the instrument file
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