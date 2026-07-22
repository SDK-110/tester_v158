using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;
using System.IO;
using unvell.ReoGrid.IO;

namespace testapp
{
  public  struct productinfo {
        string sn;
        string mac;
        string imei;
        int titlenumber;


    }
    public delegate productinfo get_productinfo(productinfo productinfo);
    public delegate void set_init_case();
    public partial class UserControl1 : UserControl
    {
        static private object lock_obj = new object();
        unvell.ReoGrid.Worksheet m_sheet =null;
        unvell.ReoGrid.Worksheet worksheet;
        testcase_table_edit _Edit;
        Dictionary<int, string> result_temp = new Dictionary<int, string>();
        private  string testcase_table = @"D:\Users\Administrator\source\repos\WindowsFormsApp2-excel\WindowsFormsApp2\bin\Debug\test.xlsx";
        private string testcase_lib = "";
        private volatile int selection_sheet ;
        private int testcase_number =1;
        private string sign_result;
        private string globe_result;
        private string ng_no_continue= "yes";
        private string serialnumber = "11111";
        private string savelog = "yyresult.csv";
        public string test_table
        
        {
            set
            {

                testcase_table = value;
            }

            get {

                return testcase_table;
            }
        
        
        }

        public string _test_lib {


            set {


                testcase_lib = value;
            }
            get {


                return testcase_lib;
            
            }
        
        
        
        }


        public UserControl1()
        {
            InitializeComponent();
            //reoGridControl1.Load(testcase_table, unvell.ReoGrid.IO.FileFormat._Auto);
            //m_sheet = reoGridControl1.Worksheets[0]; ;
            //reoGridControl1.CurrentWorksheet = m_sheet;
            //reoGridControl1.Readonly = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
           
        }

        public void set_run_Cancellation() {

            backgroundWorker1.CancelAsync();
        }
        public void loadset(int i)
        {
            selection_sheet = i;
            reoGridControl1.Load(testcase_table, FileFormat.Excel2007);
            m_sheet = reoGridControl1.Worksheets[i];
            reoGridControl1.CurrentWorksheet = m_sheet;
            reoGridControl1.Readonly = true;
            // m_sheet.SetSettings(WorksheetSettings.View_ShowHeaders, false);
            // m_sheet.SelectionMode = WorksheetSelectionMode.Row;
            //m_sheet.ScaleFactor = 1.1f;
          //  m_sheet.ScrollToRange("E1");
            m_sheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
            m_sheet.SetSettings(WorksheetSettings.View_ShowColumnHeader, false);
            m_sheet.Ranges[$"A1:F{m_sheet.Rows}"].Style.HorizontalAlign = ReoGridHorAlign.Left;
            m_sheet.SetCols(12);
            m_sheet.SetColumnsWidth(0, 1, (ushort)(reoGridControl1.Width * 0.1));
            m_sheet.SetColumnsWidth(1, 1, (ushort)(reoGridControl1.Width * 0.5));
            m_sheet.SetColumnsWidth(2, 1, (ushort)(reoGridControl1.Width * 0.1));
            m_sheet.SetColumnsWidth(3, 1, (ushort)(reoGridControl1.Width * 0.1));
            //m_sheet.SetColumnsWidth(4, 1, (ushort)(reoGridControl1.Width * 0.1));
            //m_sheet.SetColumnsWidth(5, 1, (ushort)(reoGridControl1.Width * 0.1));
            //m_sheet.SetColumnsWidth(6, 1, (ushort)(reoGridControl1.Width * 0.15));
            //m_sheet.SetColumnsWidth(7, 1, (ushort)(reoGridControl1.Width * 0.15));
            m_sheet.SetColumnsWidth(10, 1, (ushort)(reoGridControl1.Width * 0.1));
            m_sheet.SetColumnsWidth(11, 1, (ushort)(reoGridControl1.Width * 0.1));
            m_sheet.HideColumns(4, 1);
            m_sheet.HideColumns(5, 1);
            m_sheet.HideColumns(6, 1);
            m_sheet.HideColumns(7, 1);
            m_sheet.HideColumns(8, 1);
            m_sheet.HideColumns(9, 1);
            m_sheet.FreezeToCell(1, 1);
            m_sheet.Cells[i, 10].Data = "result";
            m_sheet.Cells[i, 11].Data = "time";
            testcase_number = 1;
            while (m_sheet.Cells[testcase_number, 1].Data != null)
            {
                testcase_number++;
            }
            m_sheet.SetRows(testcase_number+4);
            this.progressBar1.Maximum = testcase_number;
            this.progressBar1.Step = 1;
            if (!File.Exists(savelog))
            {
                string m, v = "time,serialno,result";

                i = 1;
                do
                {
                    
                    if (m_sheet.Cells[i, 1].Data==null) break;
                    m = m_sheet.Cells[i, 1].Data.ToString();
                    v = v + "," + m + "( " + m_sheet.Cells[i, 2].Data.ToString().Replace(",", "#") + "<-->" + m_sheet.Cells[i,3].Data.ToString().Replace(",", "#") + ")";
                    i++;

                } while (m != null && m.Trim().Length > 2);

                File.AppendAllText(savelog, v + '\n');
            }






        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {


                result_temp.Clear();
                //  worksheet.HideColumns(3, 1);

                this.Invoke(new Action(() =>
                {
                    this.progressBar1.Value = 0;
                }));
                int i = 1;
                bool runflog=true;
                globe_result = "PASS";
                while (m_sheet.Cells[i, 8].Data!=null && runflog)
                {
                   
                    
                    System.Threading.Thread.Sleep(100);
                    this.Invoke(new Action(() =>
                    {
                        
                        m_sheet.Cells[i, 6].Data =    m_sheet.Cells[i, 0].Data ;
                        sign_result = "pass";
                       // if(i==10) sign_result = "fail";
                        if (sign_result == "pass")
                        {
                            m_sheet.Cells[i, 7].Style.BackColor = Color.Green;
                            m_sheet.Cells[i, 7].Data = "PASS";
                        }
                        else {
                            m_sheet.Cells[i, 7].Style.BackColor = Color.Red;
                            m_sheet.Cells[i, 7].Data = "FAIL";
                            globe_result = "FAIL";
                        }
                        
                       reoGridControl1.Refresh();
                        m_sheet.ScrollToCell(m_sheet.Cells[i, 3]);
                        m_sheet.SelectRows(i, 1);

                        if (sign_result == "fail" || backgroundWorker1.CancellationPending == true)
                        {

                            if (ng_no_continue == "yes") { 
                                runflog = false;
                                globe_result = "FAIL";
                            }
                            else
                            {
                                sign_result = "skip";

                            }

                        }
                        else {



                            sign_result = "pass";

                        }

                        result_temp.Add(i - 1, sign_result);


                    }));
                    Application.DoEvents();

                    // System.Threading.Thread.Sleep(10);

                    this.Invoke(new Action(() =>
                    {
                        this.progressBar1.Value += 1;
                        this.richTextBox1.AppendText("debug : --> setp" + (i - 1) + ":-->" + sign_result + "\r\n");
                    }));
                    
                    i++;
                }
                

            }
            catch(Exception m)
            {

                MessageBox.Show(m.ToString());

            }
        }

        public void run_test()
        {
            if (backgroundWorker1.IsBusy) return;
            for (int i = 1; i <m_sheet.Rows; i++) {
                m_sheet.Cells[i, 6].Data = m_sheet.Cells[i, 7].Data = "";
                m_sheet.Cells[i, 7].Style.BackColor = Color.White;
            }
            
           // m_sheet.Ranges[$"F2:F{m_sheet.Rows}"].Style.BackColor = Color.White;
           
            backgroundWorker1.RunWorkerAsync();

        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressBar1.Value = this.testcase_number;
            StringBuilder s = new StringBuilder();

            s.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ",");
            //    s.Append(((this.textBox1.Text == "") ? "skip" : this.textBox1.Text) + ",");
            s.Append(((this.serialnumber== "") ? "NA" : this.serialnumber) + ",");
            s.Append((string)globe_result + ",");
            for (int a = 0; a < result_temp.Count; a++)
            {




                s.Append(result_temp[result_temp.Keys.ToArray()[a]] + ",");

            }

            s.Remove(s.Length - 1, 1);
            s.AppendLine();
            lock (lock_obj) { 
            File.AppendAllText(savelog, s.ToString());
            }
        }

        private void reoGridControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void reoGridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(this.testcase_table);
            testcase_table_edit.test_case_table = this.testcase_table;
            testcase_table_edit.selection = selection_sheet;
            testcase_table_edit.reset = delegate ()
            {

                loadset(this.selection_sheet);
            };

             _Edit = new testcase_table_edit();
             
             _Edit.ShowDialog();
        }
    }
}
