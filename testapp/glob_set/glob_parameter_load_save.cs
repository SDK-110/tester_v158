using System;
using System.Collections.Generic;
using System.Linq;
// using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace testapp.glob_set
{
    internal class glob_parameter_load_save
    {

        Dictionary<string, string> parameters;
        private static glob_parameter_load_save  instance = null;
        public static glob_parameter_load_save getInstance()
        {
            if (instance == null)
            {

                instance = new glob_parameter_load_save();

            }

            return instance;


        }
        public Dictionary<string, string> glob_params { get { return parameters; } }
        private glob_parameter_load_save() {

            parameters = new Dictionary<string, string>();


        }

    }
}
