using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbcParserLib;
using RohdeSchwarz.RsCmwLteSig;
namespace testapp.mylib
{
    internal class test_dbc
    {
        Dbc dbc = null;
        public test_dbc()
        {
          dbc = Parser.ParseFromPath("D:\\my_workspac\\temp\\DBC_\\34044-561__ENHANCED_.dbc");
        }

      public void test() {

            
            foreach (var a in dbc.Nodes) { 
            
            
            
            }
        
        }
    }
}
