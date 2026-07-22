using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Visa;
using NationalInstruments.DataInfrastructure;
using NationalInstruments.Restricted;
namespace testapp
{
    class test_visa
    {
        ResourceManager rm;
        MessageBasedSession dev;
        public test_visa()
        {
           rm = new ResourceManager();
           // var devs = rm.Find("?*INSTR");
         Ivi.Visa.IVisaSession vi =   rm.Open("USB0::0x1AB1::0x04CE::DS1ZC235207171::INSTR");
            dev = vi as MessageBasedSession;

            // dev.FormattedIO.WriteLine(":RUN");

            // string setter = $":waveform:source channel{1}";
            // dev.FormattedIO.WriteLine(setter);
            // setter = $":waveform:mode NORM";
            // dev.FormattedIO.WriteLine(setter);
            // setter = $":waveform:format BYTE";
            // dev.FormattedIO.WriteLine(setter);
            // dev.FormattedIO.WriteLine(":WAV:DATA?");

            //var p = dev.FormattedIO.ReadBinaryBlockOfByte();

            // int z = 0;
            
            dev.FormattedIO.WriteLine(":wav:pre?");

           string p = dev.FormattedIO.ReadString();

            string  setter = $":WAVeform:data?";
            dev.FormattedIO.WriteLine(setter);
            // string str_y_ref = dev.FormattedIO.ReadLine();
        //   string p = dev.FormattedIO.ReadString();
         byte[] m =    dev.FormattedIO.ReadBinaryBlockOfByte();

          string z =  string.Join(" ", m.Select(b => b.ToString("X2")));

       var hh=     m.Select(b =>
            {


              return  Math.Round(((b - double.Parse(p.Split(',')[8]) - double.Parse(p.Split(',')[9]) )* double.Parse(p.Split(',')[7])),3) ;
            }).ToArray();


            dev.FormattedIO.WriteLine(":wav:sour channel2");


            dev.FormattedIO.WriteLine(":wav:pre?");

            string p2 = dev.FormattedIO.ReadString();

            string setter2 = $":WAVeform:data?";
            dev.FormattedIO.WriteLine(setter2);
            // string str_y_ref = dev.FormattedIO.ReadLine();
            //   string p = dev.FormattedIO.ReadString();
            byte[] m2 = dev.FormattedIO.ReadBinaryBlockOfByte();

            string z2 = string.Join(" ", m2.Select(b => b.ToString("X2")));

            var hh2 = m.Select(b =>
            {


                return Math.Round(((b - double.Parse(p2.Split(',')[8]) - double.Parse(p2.Split(',')[9])) * double.Parse(p2.Split(',')[7])), 3);
            }).ToArray();



        }



    }
}
