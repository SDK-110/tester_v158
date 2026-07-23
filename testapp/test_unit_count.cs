using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ScottPlot;

namespace testapp
{
   
    public partial class test_unit_count : UserControl
    {
        
        //  static string chart_data_path = "F:\\CNTV\\Download\\tester_v78\\testapp\\bin\\Debug\\chart_data";
        public static string chart_data_path = "chart_data";
       public  passed_failed_data data_;
        public test_unit_count()
        {
            InitializeComponent();
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            
            data_ = passed_failed_data.get_json_char_data(chart_data_path);
            chart_init();


        }


        public void chart_init() {
            // ScottPlot: chart configuration is done in chart_display() method
            chart_display();

            this.label1.Text = string.Format("TOTAL：{0}PCS |NG :{1}|OK:{2}", data_.total_number, data_.total_ng_qty, data_.total_ok_qty);
        }

        public void chart_display() {
            // Chart1: PASS count per hour (green bars)
            chart1.Plot.Clear();
            double[] okData = new double[24];
            for (int i = 0; i < 24 && i < data_.intraday_24_OK_data.Length; i++)
                okData[i] = data_.intraday_24_OK_data[i];
            var bars1 = chart1.Plot.Add.Bars(okData);
            bars1.Color = ScottPlot.Colors.Green;
            bars1.LegendText = "Number of pass per hour";
            string[] hourLabels = new string[24];
            for (int i = 0; i < 24; i++) hourLabels[i] = i.ToString();
            chart1.Plot.Legend.IsVisible = true;
            chart1.Plot.Axes.Margins(bottom: 0);
            chart1.Refresh();

            // Chart2: NG count per hour (red bars)
            chart2.Plot.Clear();
            double[] ngData = new double[24];
            for (int i = 0; i < 24 && i < data_.intraday_24_NG_data.Length; i++)
                ngData[i] = data_.intraday_24_NG_data[i];
            var bars2 = chart2.Plot.Add.Bars(ngData);
            bars2.Color = ScottPlot.Colors.Red;
            bars2.LegendText = "Number of fail per hour";
            chart2.Plot.Legend.IsVisible = true;
            chart2.Plot.Axes.Margins(bottom: 0);
            chart2.Refresh();
        }
    }

  public  class passed_failed_data
    {
        static object lock_globe = new object();
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public EventHandler Handler;
        public int total_number;
        public int total_ok_qty;
        public int total_ng_qty;
        public int[] intraday_24_OK_data = new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
        public int[] intraday_24_NG_data = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };

        public void addOK(int z)
        {
            lock (lock_globe) { 
            total_ok_qty = total_ok_qty + z;
            total_number = total_number + z;
            Intraday_24_OK_data[DateTime.Now.Hour] = Intraday_24_OK_data[DateTime.Now.Hour] + z;
            if (Handler != null) Handler(null, null);
            }
        }
        public void addNG(int z)
        {
            lock (lock_globe)
            {
                total_ng_qty = total_ng_qty + z;
                total_number = total_number + z;
                Intraday_24_NG_data[DateTime.Now.Hour] = Intraday_24_NG_data[DateTime.Now.Hour] + z;
                if (Handler != null) Handler(null, null);
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int Total_QTY
        {
            set
            {

                total_number = value;

            }

            get
            {

                return total_number;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int total_ng_QTY
        {
            set
            {

                total_ng_qty = value;

            }

            get
            {

                return total_ng_qty;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int total_ok_QTY
        {
            set
            {

                total_ok_qty = value;


            }

            get
            {

                return total_ok_qty;
            }
        }
        public void  clear_data() {

       intraday_24_OK_data = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
       intraday_24_NG_data = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Total_QTY = 0;
            total_ok_QTY = 0;
            total_ng_qty = 0;
            if (Handler != null) Handler(null, null);
        }

        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int[] Intraday_24_OK_data
        {
            set
            {

                intraday_24_OK_data = value;
                if (Handler != null) Handler(null, null);


            }

            get
            {

                return intraday_24_OK_data;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int[] Intraday_24_NG_data
        {
            set
            {

                intraday_24_NG_data = value;
                if (Handler != null) Handler(null, null);
             

            }

            get
            {

                return intraday_24_NG_data;
            }
        }



        public static void save_json_char_data(passed_failed_data this_, string project_tester_name = "chart_data")
        {

           
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{project_tester_name}.json", false))
            {


                file.Write(JsonConvert.SerializeObject(this_));


            }



        }

        public static passed_failed_data get_json_char_data(string project_tester_name = "chart_data")
        {


            try
            {
                
                string p = System.IO.File.ReadAllText(project_tester_name + ".json");
               
                return JsonConvert.DeserializeObject<passed_failed_data>(p);
            }
            catch
            {

                return new passed_failed_data() { };
               
            }


        }

    }
}
