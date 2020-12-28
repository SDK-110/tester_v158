// C# example showing the synchronization of the "SING" command with the Service Request event handler
// Event handler Scope_SrqHandler() is called when the instrument generates Service Request.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RohdeSchwarz.RsInstrument;

namespace Csharp_VISA.NET_Scope_SRQevent_Example
{
    class Program
    {
        // Event handler for our Service Request Event
        private static void Scope_SrqHandler(object sender, InstrEventArgs e)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Service Request Event generated");
            Console.WriteLine("-----------------------------------");
        }
        static void Main(string[] args)
        {

            RsInstrument scope;
            try //separate try-catch for scope initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                //Initialization:
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
                scope.ClearStatus(); // Clear instrument status - errors and io buffers
                Console.WriteLine("Instrument Identification string:\n{0}", scope.Identification.IdnString);
                scope.WriteString("*RST;*CLS"); // Reset the instrument, clear the Error queue
                scope.WriteString("SYST:DISP:UPD ON"); // Display update ON - switch OFF after debugging
                //-----------------------------------------------------------
                // Basic Settings:
                //-----------------------------------------------------------
                scope.WriteString("ACQ:POIN:AUTO RECL;:TIM:RANG 2.0;:ACQ:POIN 1002;:CHAN1:STAT ON;:TRIG1:MODE AUTO"); // Define Horizontal scale by number of points
                //-----------------------------------------------------------
                // Acquisitions:
                //-----------------------------------------------------------
                Console.WriteLine("Acquisition no. 1 started...");
                scope.Events.WriteWithOpcHandler = Scope_SrqHandler;
                scope.Events.WriteStringWithOpc("SING"); // Send the SING command and call Scope_SrqHandler() when finished.
                Thread.Sleep(6000); // Wait here for invoking the handler Scope_SrqHandler()

                // Repeat
                Console.WriteLine("Acquisition no. 2 started...");
                scope.Events.WriteStringWithOpc("SING"); // Send the SING command and call Scope_SrqHandler() when finished.
                Thread.Sleep(6000); // Wait here for invoking the handler Scope_ServiceRequest()
                scope.Events.WriteWithOpcHandler = null; // remove the handler
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
