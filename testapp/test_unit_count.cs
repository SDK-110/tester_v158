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
using System.Windows.Forms.DataVisualization.Charting;

namespace testapp
{
   
    public partial class test_unit_count : UserControl
    {
        
        //  static string chart_data_path = "F:\\CNTV\\Download\\tester_v78\\testapp\\bin\\Debug\\chart_data";
        public static string chart_data_path = "chart_data";
        Series series;
        Series series2;
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

            #region //chart 绘图
            series = chart1.Series[0];
          //  series.LegendText = "每小时PASS数";
            // 画样条曲线（Spline）
            series.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series.BorderWidth = 2;
            // 线的颜色：红色
            series.Color = System.Drawing.Color.Green;
            // 图示上的文字
            series.IsValueShownAsLabel = true;
            series2 = chart2.Series[0];
         //   series2.LegendText = "每小时NG数";
            // 画样条曲线（Spline）
            series2.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series2.BorderWidth = 2;
            // 线的颜色：红色
            series2.Color = System.Drawing.Color.Red;
            // 图示上的文字
            series2.IsValueShownAsLabel = true;
            #endregion

            //this.textBox2.Left = 5;
            //this.textBox2.Top = this.Bottom-this.Height;
           
            chart_display();

         
            this.label1.Text = string.Format("TOTAL：{0}PCS |NG :{1}|OK:{2}", data_.total_number, data_.total_ng_qty, data_.total_ok_qty);

            int p = 0;
            ChartArea chartArea = chart1.ChartAreas[0];
            ChartArea chartArea2 = chart2.ChartAreas[0];

            foreach (int i in new int[] {0,1,2,3,4,5,6,7,8,9,10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,21,22,23})
            {
                CustomLabel label = new CustomLabel();
                label.Text = i + "";
                label.ToPosition = p * 2;
                chartArea.AxisX.CustomLabels.Add(label);
                chartArea.AxisX.Interval = 1;
                chartArea2.AxisX.Interval = 1;
                //CustomLabel label2 = new CustomLabel();
                //  label2.Text = "";

                //   label2.ToPosition = 1D;
                chartArea2.AxisX.CustomLabels.Add(label);



                p++;
            }

            data_.Handler = (object a, EventArgs b) =>
            {

                chart_init();
                passed_failed_data.save_json_char_data(data_);
            };
        }

        public void chart_display()
        {

           
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            if(data_==null)
            MessageBox.Show("error:data view empty");


            // 在chart中显示数据
            int x = 0;

            series.LegendText = "Number of pass per hour";
 


            foreach (float v in data_.intraday_24_OK_data)
            {
                series.Points.AddXY(x, v);
                x++;
            }


            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }


            int x2 = 0;
            series2.LegendText = "Number of fail per hour";

            foreach (float v in data_.intraday_24_NG_data)
            {
                series2.Points.AddXY(x2, v);
                x2++;
            }

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
