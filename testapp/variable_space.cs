using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace variable_space
{
    class   pycom_project
    {
        string _first_MAC = "";
       string _sec_MAC = "";
       string _lora_devui = "";
        public string first_MAC { set { _first_MAC = value; } get { return _first_MAC; } }
        public string sec_MAC { set { _sec_MAC = value; } get { return _sec_MAC; } }
        public string lpwan { set { _lora_devui = value; } get { return _lora_devui; } }
        double _tx_powr;
        double _offset;
        double _evm_peak;
        public double tx_power {

            get {

                return _tx_powr;
            }

            set {

                _tx_powr = value;

            }

        }

        public double evm_peak {
            set {

                _evm_peak = value;
            }

            get {

                return _evm_peak;

            }
        }
        public double offset
        {
            

                get {

                    return _offset;
                }

                set {

                    _offset = value;

                }

        }
        public void clr() {
            _first_MAC = "";
            _sec_MAC = "";
            _lora_devui = "";
            _tx_powr = -9999;
            _offset = 9999999999;

        }
    }

    public  class  general_buf {

        public   int item1_int;
        public  double item1_double;
        public   string item1_str="";
        public  int item2_int;
        public  double item2_double;
        public string item2_str="";
        public  int item3_int;
        public  double item3_double;
        public  string item3_str="";

    }

    public class festool_mmu_project_var {

        public string partNumber { get; set; } = "";
        public string electronicsVision { get; set; } = "";
        public string barePcbPartNumber { get; set; } = "";
        public string barePcbVersion { get; set; } = "";
        public string assembledPcbPartNumber { get; set; } = "";
        public string assembledPcbVersion { get; set; } = "";
        public string schematicPartNumbe { get; set; } = "";
        public string schematicPcbVersion { get; set; } = "";
        string Serial { get; set; } = "";
        public string ManufactureNumber { get; set; } = "";
        public string manufacturing_date {

            get;set;
        }
        public string assembledPcbManufacturingDate { get; set; }


    }
}
