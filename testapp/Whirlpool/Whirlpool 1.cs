using Code4Bugs.Utils.Types;
using MetroFramework.Controls;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.mylib;
using testapp.useful;
using unvell.ReoGrid.IO.OpenXML.Schema;

namespace testapp.whirlpool
{
    public partial class whirlpool :MetroFramework.Forms.MetroForm
    {
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        string filename = "";
        AutoResetEvent are = new AutoResetEvent(false);
        product_setting globe_setting;
        volatile int buf_flog=0;
        //Dictionary<string, string> buf = new Dictionary<string, string>();
        //CircularArray<string> buf = new CircularArray<string>(300);
        ConcurrentQueue<string> buf = new ConcurrentQueue<string>();
        ConcurrentQueue<string> buf_log = new ConcurrentQueue<string>();
        luoyinji lyj;
        public whirlpool()
        {
            InitializeComponent();


            dataGridView1.VirtualMode = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            //  lyj.WriteLine("K_ESC");
            //  lyj.send_command("K_TEST");
            //  lyj.WriteLine("K_TEST");
            // System.Threading.Thread.Sleep(300);
            //  lyj.WriteLine("K_TEST");
            // new sqlite_handle("TestData");

            // get_sqlite_table_toview();

            // new FestoolCom("com3", 9600);

          //  buf.clear();


        }

  

     public  class product_setting { 
        
        public string Maufacture { set; get; } = "Season SDK";
        public string Product_name { set; get; } = "Auto Line";
        
         public string PIC { set; get; } = "Isia";
         public string Operator { set; get; } = "Tom";
         public string  Test_Filename { set; get; } = "AUTO_12345";

            public int tested { get {

                    return passed + failed;
                } 
            }
            public int passed { set; get; } = 0;
            public int failed { set; get; } = 0;
            public string last_sn { set; get; } = "AUTO_12345";

            public string sn_reg { set; get; } = @"\d+";
            public string COM_PORT { set; get; } = @"COM1;9600";
        }

        private void monitoring1_FormClosing(object sender, FormClosingEventArgs e)
        {
            testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");
        }

        private void monitoring1_Load(object sender, EventArgs e)
        {
            this.metroTabControl1.SelectedIndex = 2;
            this.metroDateTime1.Value = this.metroDateTime1.Value.AddDays(-3);
            this.metroDateTime2.Value = DateTime.Now;

         //   Task.Factory.StartNew(() =>
         //   {

                this.Invoke(new Action(() =>
                {
                    if (!File.Exists("setup_p.xml"))
                    {
                        if (globe_setting == null) globe_setting = new product_setting();
                        testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");

                    }
                    if (globe_setting == null) globe_setting = testapp.useful.XmlHelper.DeserializeFromXml<product_setting>("setup_p.xml");


                    update_ui();
                    this.label1.Text = "WAIT";
                    this.label1.ForeColor = System.Drawing.Color.Blue;
                }));


          //  });

        }


        public void update_ui() {

            this.label3.Text = globe_setting.tested.ToString();
            this.label4.Text = globe_setting.passed.ToString();
            this.label6.Text = globe_setting.failed.ToString();
            percentageProgressBar1.Maximum = percentageProgressBar2.Maximum= percentageProgressBar3.Maximum = (globe_setting.tested == 0) ? 1 : globe_setting.tested;
            percentageProgressBar1.Value = globe_setting.passed;
            percentageProgressBar2.Value = globe_setting.passed;
            percentageProgressBar3.Value = globe_setting.failed;
            PIC.Text = globe_setting.PIC;
            this.Product_Name.Text = globe_setting.Product_name;
            this.Maufacturer.Text = globe_setting.Maufacture;
            this.test_file_name.Text = globe_setting.Test_Filename;
            this.Operator.Text = globe_setting.Operator;
            this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename} | Operator: {globe_setting.Operator} | last_SN: {globe_setting.last_sn}";
            this.label20.Text = $"PN:{globe_setting.Product_name}";



        }
        private void roundButton2_Click(object sender, EventArgs e)
        {

            globe_setting.PIC = PIC.Text;
            globe_setting.Product_name = this.Product_Name.Text;
            globe_setting.Maufacture = this.Maufacturer.Text;
            globe_setting.Operator = this.Operator.Text;
            globe_setting.Test_Filename = this.test_file_name.Text;
            testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) {
            
           
            MatchCollection reg = new Regex(globe_setting.sn_reg).Matches(this.metroTextBox1.Text);
            if(reg.Count==0) return;
            richTextBox1.Text = string.Empty;
            metroTextBox1.Enabled = false;
            globe_setting.last_sn = this.metroTextBox1.Text.Trim().Replace("\n","").Replace("\r", "");
                while (buf_log.TryDequeue(out _)) ;
                    buf_flog = 0;
            label1.Text = "RUN";
            lyj.WriteLine("K_TEST");
                int cout = 50;
                do
                {
                    await Task.Delay(100);

                } while (buf_flog != 2 && cout-->0);

            string tmp = "";
            sqlite_handle sqh = new sqlite_handle();
            string pass_fail_flog = "PASS";
            string tmp2 = "";
                if (buf_log.Count > 2)
                {
                    while (buf_log.TryDequeue(out tmp))
                    {
                        if (tmp.ToUpper().IndexOf("FAIL") >= 0) { pass_fail_flog = "Fail"; tmp2 = "FAIL"; } else { tmp2 = "PASS"; }
                        sqh.InsertRecord(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), globe_setting.last_sn, globe_setting.Test_Filename, tmp2, globe_setting.Maufacture,
                            globe_setting.Product_name, globe_setting.PIC, globe_setting.Operator, tmp);
                     
                    }
                    sqh.commit();
                }
                else {
                    this.label1.Text = "EMPTY";
                    this.label1.ForeColor = Color.Red;
                    pass_fail_flog = "Fail";
                }

                this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename} | Operator: {globe_setting.Operator} | last_SN: {globe_setting.last_sn}";
            if (pass_fail_flog == "Fail") { 
                this.label1.Text = "FAIL"; 
                this.label1.ForeColor = Color.Red;
                globe_setting.failed += 1;
               

            } else { 
                this.label1.Text = "PASS"; 
                this.label1.ForeColor = Color.LightGreen;
                globe_setting.passed += 1;
            }


            update_ui();
            metroTextBox1.Text=String.Empty;
            metroTextBox1.Enabled = true;
            metroTextBox1.Focus();
            }
        }

        private void monitoring1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }


        void get_sqlite_table_toview() {


            if (!File.Exists("TestData.db")) {

                this.Invoke(new Action(() =>
                {
                   // this.roundButton3.Text = "Done";
                }));
                    return; }
           StringBuilder  bl= new StringBuilder();  
            string connectionString = "Data Source=TestData.db;Version=3;";
         
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT * FROM TestData where DateTimeT >= '{metroDateTime1.Value.ToString("yyyy-MM-dd")}' and  DateTimeT <= '{metroDateTime2.Value.ToString("yyyy-MM-dd")}' and BarCode like '%{((this.metroTextBox2.Text==string.Empty)?"":this.metroTextBox2.Text)}%'";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        this.Invoke(new Action(() =>
                        {
                            dataGridView1.DataSource = table;
                            //  bl.Append(table.ToJson());
                          //  MiniExcelLibs.MiniExcel.SaveAs("123.csv",table, excelType: MiniExcelLibs.ExcelType.CSV);
                            
                          //  this.roundButton3.Text = "Done";
                        }));
                        


                 
                    
                   
                     
                       
                       

                 
                    }
                }
            }



        }


        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as MetroTabControl).SelectedIndex == 1) {
                if (globe_setting == null) return;
                this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename} | Operator: {globe_setting.Operator} | latest_SN: {globe_setting.last_sn}";

            }
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            this.roundedButton3.Text = "Query...";

            Task.Factory.StartNew(() =>
            {


                get_sqlite_table_toview();

            });
           

        }


        static void SaveDataTableToCsv(DataTable dataTable, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the column headers
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    writer.Write(dataTable.Columns[i].ColumnName);
                    if (i < dataTable.Columns.Count - 1)
                    {
                        writer.Write(",");
                    }
                }
              

                // Write the data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString());
                        if (i < dataTable.Columns.Count - 1)
                        {
                            writer.Write(",");
                        }
                    }
                   
                }
            }

        }

        private void monitoring1_Shown(object sender, EventArgs e)
        {
            FlashWindow(Handle, true);
           // roundButton1.Enabled = false;
            metroTextBox1.Enabled = false;
            lyj = new luoyinji(globe_setting.COM_PORT.Split(';')[0]);
            lyj.get_msg = (o) =>
            {

                this.Invoke((Action)delegate
                {
                    string tmp = o.Replace("$a", "").Replace("?[", "Ω");
                    if (tmp.IndexOf("Start Test")>=0){

                        buf_flog = 1;

                    }

                  
                    if (buf_flog==1) {
                       buf_log.Enqueue(tmp);
                    }

                    if (tmp.IndexOf("End Test") >= 0)
                    {

                        buf_flog = 2;
                    }


                    buf.Enqueue(tmp);
                    if (buf.Count > 500)
                    {
                        int itemsToRemove = buf.Count - 300;
                        string[] itemsToRemoveArray = new string[itemsToRemove];
                        // 移除要保留的元素之前的元素
                        for (int i = 0; i < itemsToRemove; i++)
                        {
                            buf.TryDequeue(out itemsToRemoveArray[i]);
                        }
                    }
                    this.richTextBox1.AppendText(tmp + "\n");

                });

            };
            are = new AutoResetEvent(true);
            timer1.Start();
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (are.WaitOne(0))
            {
                label19.Text = $"Current document NOT Found";
                label19.ForeColor = Color.Red;
                Task.Factory.StartNew(() => {  
                // buf.Clear();
                lyj.WriteLine("K_ESC");
                     Task.Delay(100).Wait();
                    lyj.WriteLine("K_ESC");
                    Task.Delay(100).Wait();
                    lyj.WriteLine("K_TEST");
                string tmp = "";
                    if (buf.Count > 0) {

                        for (int i = 0; i < buf.Count; i++) {

                            buf.TryDequeue(out tmp);
                            if (tmp.IndexOf("File:") >= 0) {
                                while (buf_log.TryDequeue(out _))
                                    timer1.Enabled = false;
                                filename = tmp;
                                this.Invoke(new Action(()=> {

                                    label19.Text = $"Current document is :{filename.Substring(5)} {globe_setting.Test_Filename}";
                                    if (filename.Substring(5) != globe_setting.Test_Filename)
                                    {
                                        label19.Text = $"Current document is :{filename.Substring(5)}, It is incorrect. ";
                                        label19.ForeColor = Color.Red;
                                        //   MessageBox.Show(" Current document ERROR,  app is about to exit ");
                                        // Application.Exit();
                                    }
                                    else {

                                        label19.ForeColor = Color.Green;
                                        //roundButton1.Enabled = true;
                                        metroTextBox1.Enabled = true;
                                    }
                                }));
                               
                                
                                break;
                            }
                        }
                    
                    }
                  
                    are.Set();
                });

            }

        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
           
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            //// 根据需要提供数据行的值
            //// 这里可以根据具体的逻辑从数据源中获取数据
            //object value = GetDataValue(rowIndex, columnIndex);

            //// 将值赋给e.Value
            //e.Value = value;
        }

     
    }
}
