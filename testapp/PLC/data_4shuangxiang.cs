using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace testapp.PLC
{
    [Serializable]
    [XmlRoot("tester_standard_style")]
    class data_4shuangxiang : INotifyPropertyChanged
    {

        [XmlIgnore]
        private static data_4shuangxiang _obj;

        
        [XmlIgnore]
        private string m_Filed = "DefaultValue";
        [XmlElement(" Field")]
        public string Field
        {
            get
            {
                return this.m_Filed;
            }
            set
            {
                this.m_Filed = value;
                this.SendChangeInfo("Field");
            }
        }

        private void SendChangeInfo(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private data_4shuangxiang()
        {
            m_Filed = "000000000";
        }
        [XmlIgnore]
        private decimal m_Decimal = (decimal)60.01;
        [XmlElement(" Decimal")]
        public decimal Decimal
        {
            get
            {
                return this.m_Decimal;
            }
            set
            {
                this.m_Decimal = value;
                this.SendChangeInfo("Decimal");
            }
        }
        [XmlIgnore]
        private bool m_Bool = false;
        [XmlElement(" Bool")]
        public bool Bool
        {
            get
            {
                return this.m_Bool;
            }
            set
            {
                this.m_Bool = value;
                this.SendChangeInfo("Bool");
            }
        }
        public static void save_xml_files(data_4shuangxiang sav, string savename = "interface_1")
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{savename}.xml", false))
            {



                file.Write(mylib.utility_func.Serializer<data_4shuangxiang>(sav));


            }



        }



        public static data_4shuangxiang red_xml_test_cases(string test_case_xml_path = "interface_1")
        {

            try
            {
                if (_obj == null)
                {
                    string p = System.IO.File.ReadAllText(test_case_xml_path + ".xml");
                    if (p.Length <= 0) throw new Exception("error");
                    _obj = mylib.utility_func.Deserialize<data_4shuangxiang>(p);
                    if (_obj == null)
                    {
                        System.Windows.Forms.MessageBox.Show("deserialize error!!!");
                        File.Delete("test_case_xml_path" + ".xml");
                        throw new Exception("error");
                    }
                }

                return _obj;

            }
            catch
            {


                var z = new data_4shuangxiang();
                _obj = z;
                return _obj;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void save_files()
        {

            save_xml_files(this);
        }

        ~data_4shuangxiang()
        {
            save_xml_files(this);


        }
    }
}
