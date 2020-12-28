// Example showing a simple opening of a single CMW base driver session, plus
// adjusting some basic driver settings
// Another specialty is the Reliability interface - used in all CMW drivers

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBase;

namespace RsCmwBase_Example
{
    class Program
    {
        static void Main()
        {
            // Create a new session for the Base driver
            var cmwBase = new RsCmwBase("TCPIP::localhost::INSTR", true, true);
            Console.WriteLine($"CMW Base IDN '{cmwBase.Utilities.Identification.IdnString}'");

            // Utilities settings
            Console.WriteLine($"CMW Instrument options:\n{string.Join(",", cmwBase.Utilities.Identification.InstrumentOptions)}'");
            cmwBase.Utilities.VisaTimeout = 5000;

            // Sends OPC after each command
            cmwBase.Utilities.OpcQueryAfterEachSetting = false;

            // Checks for syst:err? after each command / query
            cmwBase.Utilities.InstrumentStatusChecking = true;

            // Selftest
            var selftest = cmwBase.Utilities.SelfTest();
            Console.WriteLine("CMW selftest result: {0} - {1}", selftest, selftest == 0 ? "Passed" : "Failed");

            // Driver's Interface reliability offers a convenient way of reacting on the return value Reliability Indicator
            cmwBase.Reliability.ExceptionOnError = true;

            // We register a callback for each change in the reliability indicator
            cmwBase.Reliability.Updated += (sender, eventArgs) =>
                Console.WriteLine($"Base Reliability updated.\nContext: {eventArgs.Context}\nMessage: {eventArgs.Message}");

            // You can obtain the last value of the returned reliability
            Console.WriteLine($"\nReliability last value: {cmwBase.Reliability.LastValue}, context '{cmwBase.Reliability.LastContext}', message: {cmwBase.Reliability.LastMessage}");

            // Reference Frequency Source
            cmwBase.System.Reference.Frequency.Source = SourceIntExtEnum.INTernal;

            Console.WriteLine("\n\nPress any key ...");
            Console.ReadKey();
        }
    }
}