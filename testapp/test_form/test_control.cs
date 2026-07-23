using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp
{
    public partial class test_control : Form
    {



        public test_control()
        {
            InitializeComponent();
            //  lib.chuandi(this);
            //  this.button1.DataBindings.Add("Text", data_4, "Field");
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //mydata_gridview1.set_init_4runlib_testcase(ref lib);
            //mydata_gridview1.set_production_info(new production_info() { log_path_name = "123.csv" });
            this.tabControl1.Region = new Region(new RectangleF(this.tabPage1.Left, this.tabPage1.Top, this.tabPage1.Width, this.tabPage1.Height));
            this.toolStripButton3.Font = new Font(testapp.useful.IconfontHelper.PFCC.Families[0], 16);
            this.toolStripButton3.Text = "\uF5B0";
            this.toolStripButton2.Font = new Font(testapp.useful.IconfontHelper.PFCC.Families[0], 16);
            this.toolStripButton2.Text = "\uF5AD";
    


        }


        private void abc(string v)
        {
            //this.Invoke(new Action(() => { 

            //this.richTextBox1.AppendText(v + "\n\r"); 

            //}));
        }


        public string abcd
        {

            set
            {

                this.knobControl1.Value = int.Parse(value);

            }
            get
            {

                return this.knobControl1.Value + "";

            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            // mydata_gridview1.run();
        }

        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form5_Resize(object sender, EventArgs e)
        {
            //mydata_gridview1.resize();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data_4.Field = DateTime.Now.ToString();


            Parallel.For(0, 100, i =>
            {
                mylib.mul_thread_works.insert_work(new string[] {

                i+"","rewqrewwe","dsfsdafds"
            });
            });

        }

        testcase_dll lib = new testcase_dll();
        PLC.data_4shuangxiang data_4 = PLC.data_4shuangxiang.red_xml_test_cases();

        private void uiButton1_Click(object sender, EventArgs e)
        {
            //  mydata_gridview1.run();
        }
        int m = 0;
        double[] x = new double[100];
        double[] y = new double[100];
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.sevenSegment1.Value = DateTime.Now.ToString("-ss");


            Random rd = new Random();

            for (int t = 0; t < 100; t++)
            {


                x[t] = t + m;
                y[t] = rd.Next(-10, 30);
            }

            this.easyChartX1.Plot.Clear();
            this.easyChartX1.Plot.Add.Signal(y);
            this.easyChartX1.Refresh();

            m++;


            mylib.mul_thread_works.do_background_task();
        }



        #region 窗体拖动
        private static bool IsDrag = false;
        private int enterX;
        private int enterY;
        private void setForm_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrag = true;
            enterX = e.Location.X;
            enterY = e.Location.Y;
        }
        private void setForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.Location.X - enterX;
                Top += e.Location.Y - enterY;
            }
        }
        #endregion

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            e.Graphics.DrawString(" ", e.Font, Brushes.White, e.Bounds);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void userButton1_Click(object sender, EventArgs e)
        {

            int z = 12;
            //this.uiNavMenu1.Nodes[0].Text = "\uF5AD";
            //this.uiNavMenu1.Nodes[0].ForeColor = Color.Red;
            Byte[] a = System.Text.ASCIIEncoding.ASCII.GetBytes("1234".ToCharArray());
            var m = BitConverter.ToUInt32(a,0);
            //var t = BitConverter.ToUInt32(System.Text.ASCIIEncoding.ASCII.GetBytes("1234".ToCharArray().Reverse().ToArray()), 0);

            var p = str_to_manufactureNumber_4b("1234");

            MessageBox.Show(m+"");
        }

        public uint str_to_manufactureNumber_4b(string mfn)
        {
            string m = mfn;
            return BitConverter.ToUInt32(new byte[] { (byte)m[0], (byte)m[1], (byte)m[2], (byte)(byte)m[3] }, 0); ;
        }
        /// <summary>
        /// 用于将内存的uint 4字节 byte 转成字符串
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string uint_to_str_formanufacturenumber(uint b)
        {


            byte[] v = BitConverter.GetBytes(b);

            //	Console.WriteLine($"{v[3]:c}{v[2]:c}{v[1]:c}{v[0]:c}");
            return $"{(char)v[0]}{(char)v[1]}{(char)v[2]}{(char)v[3]}";
        }


        private void roundButton1_Click(object sender, EventArgs e)
        {
            hollowCircularProgressControl1.Progress += 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }


}
