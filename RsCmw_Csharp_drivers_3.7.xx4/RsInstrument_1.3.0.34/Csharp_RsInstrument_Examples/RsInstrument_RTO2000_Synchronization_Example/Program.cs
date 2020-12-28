// C# example showing the synchronization with the STB polling and WaitForSrq of:
// "SING" command, the program waits for the acquisition to finish
// "*TST?" query, the program waits for the selftest to finish and then reads the result of the selftest
// Use this example for Service Request waiting by changing the RsInstrument object constructor (see below)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RohdeSchwarz.RsInstrument; // .NET component providing all the necessary VISA extended functionalities

namespace RsInstrument_RTO2000_Synchronization_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            RsInstrument scope;
            try //separate try-catch for scope initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                //Initialization:
                //-----------------------------------------------------------
                // Adjust the VISA Resource string to fit your instrument
                // For SRQ waiting, use the following constructor:

                //scope = new RsInstrument("TCPIP::10.212.1.131::INSTR", true, true, "OpcWaitMode=ServiceRequest");
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

            try // try block to catch any InstrumentErrorException() or InstrumentOPCtimeoutException()
            {
                Console.WriteLine("RsInstrument Driver Version: {0}, Core Version: {1}", scope.Identification.DriverVersion, scope.Identification.CoreVersion);
                scope.ClearStatus(); // Clear instrument io buffers
                Console.WriteLine("Instrument Identification string:\n{0}", scope.Identification.IdnString);
                scope.WriteString("SYST:DISP:UPD ON"); // Display update switched ON
                //-----------------------------------------------------------
                // Settings all in one string:
                //-----------------------------------------------------------
                scope.WriteString("ACQ:POIN:AUTO RECL;:TIM:RANG 2.0;:ACQ:POIN 1002;:CHAN1:STAT ON;:TRIG1:MODE AUTO");
                //-----------------------------------------------------------
                // Acquisition:
                //-----------------------------------------------------------
                // Sending SCPI command SING and using STB polling synchonization, timeout 6000 ms
                Console.Write("Acquisition started ... ");
                scope.WriteStringWithOpc("SING", 6000);
                Console.WriteLine("finished");
                //-----------------------------------------------------------
                // Selftest:
                //-----------------------------------------------------------
                // Synchronizing of a long-lasting command
                Console.Write("Selftest started ... ");
                scope.QueryStringWithOpc("*TST?", 120000);
                Console.WriteLine("finished");
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
