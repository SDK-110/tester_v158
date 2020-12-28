// Example showing on how to open a single VISA session to the instrument and use it in 3 differenct drivers

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RohdeSchwarz.RsCmwBase;
using RohdeSchwarz.RsCmwGprfGen;
using RohdeSchwarz.RsCmwGprfMeas;

namespace DriversCoExistence
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Open a new physical VISA session to the instrument
                var cmwBase = new RsCmwBase("TCPIP::localhost::INSTR");

                // Reuse the cmwBase session for the RsCmwGprfGen
                var cmwGprfGen = new RsCmwGprfGen(cmwBase.Session);

                // Reuse the cmwBase session for the RsCmwGprfMeas
                var cmwGprfMeas = new RsCmwGprfMeas(cmwBase.Session);
            }
            
            catch(Exception ex) when (
                ex is RohdeSchwarz.RsCmwBase.InstrumentStatusException ||
                ex is RohdeSchwarz.RsCmwGprfGen.InstrumentStatusException ||
                ex is RohdeSchwarz.RsCmwGprfMeas.InstrumentStatusException)
            {
                Console.WriteLine($"{ex.Message}");
            }

            finally
            {
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }

        }
    }
}
