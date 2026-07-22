using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.glob_set
{
    public class glob_ini_instance
    {
        object lock_ojbect = new object();
        private static glob_ini_instance instance = null;
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        private IniParser.Model.IniData setup_ini_data;
        public static glob_ini_instance  getInstance() {

            if (instance == null)
            {

                instance = new glob_ini_instance();
                
            }
      
            return instance;
        
        
        }

        private glob_ini_instance() {

            setup_ini_data = iniread.ReadFile("setup.ini"); //实例化ini文件
        }
            
        public IniParser.Model.IniData getSetupIniData { get { return setup_ini_data; } }
        public IniParser.FileIniDataParser fileIni { get { return iniread; } }

        public void write2Ini(IniParser.Model.IniData inidata)
        {

            lock (lock_ojbect) {


                iniread.WriteFile("setup.ini", inidata);

            }
        }
    }
}
