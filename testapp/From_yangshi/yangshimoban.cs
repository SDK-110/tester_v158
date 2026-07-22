using IniParser;
using rebuild.testcase_loader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.useful;
using 重构程序.testcase_loader;

namespace testapp.From_yangshi
{
    public partial class yangshimoban : Form
    {
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        IniParser.Model.IniData dt;
        System.Drawing.Text.PrivateFontCollection  pfcc = IconfontHelper.LoadFont();
        private Point mouseLocation;//表示鼠标对于窗口左上角的坐标的负数
        private bool isDragging;//标识鼠标是否按下
        tester_project mu;
        led_assy led_asy = null;
        sevy_relay relay1 = null;
        sevy_relay relay2 = null;
        int[] led = new int[20];
        int[] pcbastatus = new int[5] { -1, -1, -1, -1, -1};

        int[,] hi = new int[6, 4];
        int[,] low = new int[6, 4];
        int[] _dis = { 1, 1, 1, 5 };
        int[,] group = new int[5, 6];
        volatile int  glob_flog=0;
        public yangshimoban()
        {
            InitializeComponent();
     
            #region  加载配置
            dt = iniread.ReadFile("led_set.ini");
           
            string set_str = "";
             set_str = dt["setparameter"]["led1_hi"];
            hi[0, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim());hi[0, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[0, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim());hi[0, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led2_hi"];
            hi[1, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); hi[1, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[1, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); hi[1, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led3_hi"];
            hi[2, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); hi[2, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[2, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); hi[2, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led4_hi"];
            hi[3, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); hi[3, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[3, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); hi[3, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led5_hi"];
            hi[4, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); hi[4, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[4, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); hi[4, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led6_hi"];
            hi[5, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); hi[5, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            hi[5, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); hi[5, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            /////////////////==============///////////////////================//////////////
            set_str = dt["setparameter"]["led1_low"];
            low[0, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[0, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[0, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[0, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led2_low"];
            low[1, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[1, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[1, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[1, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led3_low"];
            low[2, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[2, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[2, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[2, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led4_low"];
            low[3, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[3, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[3, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[3, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led5_low"];
            low[4, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[4, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[4, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[4, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led6_low"];
            low[5, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); low[5, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            low[5, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); low[5, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led_Off"];
            _dis[0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); _dis[1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            _dis[2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); _dis[3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            set_str = dt["setparameter"]["led_group1"];
            group[0, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); group[0, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            group[0, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); group[0, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            group[0, 4] = int.Parse(set_str.Split(",".ToArray())[4].Trim()); group[0, 5] = int.Parse(set_str.Split(",".ToArray())[5].Trim());
            set_str = dt["setparameter"]["led_group2"];
            group[1, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); group[1, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            group[1, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); group[1, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            group[1, 4] = int.Parse(set_str.Split(",".ToArray())[4].Trim()); group[1, 5] = int.Parse(set_str.Split(",".ToArray())[5].Trim());
            set_str = dt["setparameter"]["led_group3"];
            group[2, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); group[2, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            group[2, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); group[2, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            group[2, 4] = int.Parse(set_str.Split(",".ToArray())[4].Trim()); group[2, 5] = int.Parse(set_str.Split(",".ToArray())[5].Trim());
            set_str = dt["setparameter"]["led_group4"];
            group[3, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); group[3, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            group[3, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); group[3, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            group[3, 4] = int.Parse(set_str.Split(",".ToArray())[4].Trim()); group[3, 5] = int.Parse(set_str.Split(",".ToArray())[5].Trim());
            set_str = dt["setparameter"]["led_group5"];
            group[4, 0] = int.Parse(set_str.Split(",".ToArray())[0].Trim()); group[4, 1] = int.Parse(set_str.Split(",".ToArray())[1].Trim());
            group[4, 2] = int.Parse(set_str.Split(",".ToArray())[2].Trim()); group[4, 3] = int.Parse(set_str.Split(",".ToArray())[3].Trim());
            group[4, 4] = int.Parse(set_str.Split(",".ToArray())[4].Trim()); group[4, 5] = int.Parse(set_str.Split(",".ToArray())[5].Trim());
      
            #endregion

        }
        #region 关闭按钮样式事件

        private void yangshimoban_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseLocation = new Point(-e.X, -e.Y);
                //表示鼠标当前位置相对于窗口左上角的坐标，
                //并取负数,这里的e是参数，
                //可以获取鼠标位置
                isDragging = true;//标识鼠标已经按下
            }

        }

        private void yangshimoban_MouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                Point newMouseLocation = MousePosition;
                //获取鼠标当前位置
                newMouseLocation.Offset(mouseLocation.X, mouseLocation.Y);
                //用鼠标当前位置加上鼠标相较于窗体左上角的
                //坐标的负数，也就获取到了新的窗体左上角位置
                Location = newMouseLocation;//设置新的窗体左上角位置
            }


        }

        private void yangshimoban_MouseUp(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                isDragging = false;//鼠标已抬起，标识为false
            }

        }

        private void yangshimoban_Load(object sender, EventArgs e)
        {
            try
            {
                led_asy = new led_assy(dt["setparameter"]["led_port"]);
            }
            catch (Exception en)
            {

                MessageBox.Show(en.ToString());
                this.Close();

            }
            try
            {
             //   relay1 = new sevy_relay(dt["setparameter"]["relay1_port"]);
            }
            catch (Exception en)
            {

                MessageBox.Show(en.ToString());
                this.Close();

            }



            //      mu = excel2tester_standard.read_excel_test_cases("project_tester_name.dll");



        }

        private void yangshimoban_Shown(object sender, EventArgs e)
        {
           
            label1.Text = "\uF156";
            label1.Font = new Font(pfcc.Families[0], 20);
            label1.ForeColor = Color.WhiteSmoke;
        }

    
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }
        private void label1_MouseHover(object sender, EventArgs e)
        {

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.WhiteSmoke;
        }

        #endregion


        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
            this.richTextBox3.Text = "";
            this.richTextBox4.Text = "";
            this.richTextBox5.Text = "";
            label8.Text = "RUNNING....";
            label9.Text = "RUNNING....";
            label10.Text = "RUNNING....";
            label2.Text = "RUNNING....";
            label3.Text = "RUNNING....";
            label8.ForeColor = Color.Yellow;
            label9.ForeColor = Color.Yellow;
            label10.ForeColor = Color.Yellow;
            label2.ForeColor = Color.Yellow;
            label3.ForeColor = Color.Yellow;
            button1.BackColor = Color.Yellow;
            button1.Enabled = false;
            Task.Factory.StartNew(() => {

            for (int saomiao = 0; saomiao < 6; saomiao++)
            {
                    realyset(saomiao + 1);
                    if (led_asy.get_all_channel_data(ref led) != 1) {
                        this.Invoke((Action)delegate ()
                        {
                            label8.Text = "ERROR";
                            label9.Text = "ERROR";
                            label10.Text = "ERROR";
                            label2.Text = "ERROR";
                            label3.Text = "ERROR";
                            label8.ForeColor = Color.Black;
                            label9.ForeColor = Color.Black; ;
                            label10.ForeColor = Color.Black; ;
                            label2.ForeColor = Color.Black;
                            label3.ForeColor = Color.Black; ;
                            button1.BackColor = Color.Blue ;

                            button1.Enabled = true;

                        });
                        return;
                    };

                int flog = 1;
                int[] rsu = led;

                for (int pcba = 0; pcba < 5; pcba++)
                {

                    if (saomiao != 0)
                    {
                        if (pcbastatus[pcba] != 1) continue;
                    }
                    flog = 1;
                    for (int uled = 0; uled < 6; uled++)
                    {

                        int r = rsu[group[pcba, uled] * 4 + 0];
                        int g = rsu[group[pcba, uled] * 4 + 1];
                        int b = rsu[group[pcba, uled] * 4 + 2];
                        int w = rsu[group[pcba, uled] * 4 + 3];
                        switch (pcba)
                        {

                            case 0:
                                {
                                    this.Invoke((Action)delegate ()
                                    {
                                        this.richTextBox1.AppendText((pcba + 1) + ":" + (uled + 1) + "[ " + r + "," + g + "," + b + "," + w + "]" + "\n");


                                    });

                                }
                                break;
                            case 1:
                                {
                                    this.Invoke((Action)delegate ()
                                    {
                                        this.richTextBox2.AppendText((pcba + 1) + ":" + (uled + 1) + "[ " + r + "," + g + "," + b + "," + w + "]" + "\n");

                                    });
                                }
                                break;
                            case 2:
                                {
                                    this.Invoke((Action)delegate ()
                                    {
                                        this.richTextBox3.AppendText((pcba + 1) + ":" + (uled + 1) + "[ " + r + "," + g + "," + b + "," + w + "]" + "\n");
                                    });
                                }
                                break;
                            case 3:
                                {
                                    this.Invoke((Action)delegate ()
                                    {
                                        this.richTextBox4.AppendText((pcba + 1) + ":" + (uled + 1) + "[ " + r + "," + g + "," + b + "," + w + "]" + "\n");
                                    });
                                }
                                break;
                            case 4:
                                {
                                    this.Invoke((Action)delegate ()
                                    {
                                        this.richTextBox5.AppendText((pcba + 1) + ":" + (uled + 1) + "[ " + r + "," + g + "," + b + "," + w + "]" + "\n");
                                    });
                                }
                                break;



                        }
                        if (uled == saomiao)
                        {
                            if (!(r <= hi[uled, 0] && g <= hi[uled, 1] && b <= hi[uled, 2] && w <= hi[uled, 3] &&
                                  r >= low[uled, 0] && g >= low[uled, 1] && b >= low[uled, 2] && w >= low[uled, 3]
                             ))
                            {

                                flog = -1;
                                break;

                            }


                        }
                        else
                        {
                                int bjw = 0;
                                for (int fdd = 0; fdd < 6; fdd++)
                                {

                                    if (group[pcba, uled] == group[pcba, fdd])
                                    {

                                        bjw = 1;
                                    }
                                }


                                if ((r > _dis[0] || g > _dis[1] || b > _dis[2] || w > _dis[3]) && bjw==0)
                            {

                                flog = -1;
                                break;
                            }


                        }



                    }
                        if (flog == 1)
                        {


                            pcbastatus[pcba] = 1;
                        }
                        else {

                            pcbastatus[pcba] = -1;

                        }

                }

            }



            for (int prr2 = 0; prr2 < 5; prr2++)
            {




                switch (prr2)
                {

                    case 0:
                        {
                            if (pcbastatus[prr2] == 1)
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    label8.Text = "RESULT :\uF854";
                                    label8.Font = new Font(pfcc.Families[0], 30);
                                    label8.ForeColor = Color.Green;
                                });
                            }
                            else
                            {

                                this.Invoke((Action)delegate ()
                                {
                                    label8.Text = "RESULT :\uF158 ";
                                    label8.Font = new Font(pfcc.Families[0], 30);
                                    label8.ForeColor = Color.Red;
                                });

                            }

                        }
                        break;
                    case 1:
                        {

                            if (pcbastatus[prr2] == 1)
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    label9.Text = "RESULT :\uF854";
                                    label9.Font = new Font(pfcc.Families[0], 30);
                                    label9.ForeColor = Color.Green;
                                });
                            }
                            else
                            {

                                this.Invoke((Action)delegate ()
                                {
                                    label9.Text = "RESULT :\uF158 ";
                                    label9.Font = new Font(pfcc.Families[0], 30);
                                    label9.ForeColor = Color.Red;
                                });
                            }
                        }
                        break;
                    case 2:
                        {
                            if (pcbastatus[prr2] == 1)
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    label10.Text = "RESULT :\uF854";
                                    label10.Font = new Font(pfcc.Families[0], 30);
                                    label10.ForeColor = Color.Green;
                                });
                            }
                            else
                            {

                                this.Invoke((Action)delegate ()
                                {
                                    label10.Text = "RESULT :\uF158 ";
                                    label10.Font = new Font(pfcc.Families[0], 30);
                                    label10.ForeColor = Color.Red;
                                });

                            }

                        }
                        break;
                    case 3:
                        {
                            if (pcbastatus[prr2] == 1)
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    label3.Text = "RESULT :\uF854";
                                    label3.Font = new Font(pfcc.Families[0], 30);
                                    label3.ForeColor = Color.Green;
                                });
                            }
                            else
                            {

                                this.Invoke((Action)delegate ()
                                {
                                    label3.Text = "RESULT :\uF158 ";
                                    label3.Font = new Font(pfcc.Families[0], 30);
                                    label3.ForeColor = Color.Red;
                                });

                            }

                        }
                        break;
                    case 4:
                        {
                            if (pcbastatus[prr2] == 1)
                            {
                                this.Invoke((Action)delegate ()
                                {
                                    label2.Text = "RESULT :\uF854";
                                    label2.Font = new Font(pfcc.Families[0], 30);
                                    label2.ForeColor = Color.Green;
                                });
                            }
                            else
                            {

                                this.Invoke((Action)delegate ()
                                {
                                    label2.Text = "RESULT :\uF158 ";
                                    label2.Font = new Font(pfcc.Families[0], 30);
                                    label2.ForeColor = Color.Red;
                                });

                            }

                        }
                        break;












                }


            }









            this.Invoke((Action)delegate(){
                realycleaar();
                button1.Enabled = true;
                button1.BackColor = Color.Blue;

                string m = "";
                m = m + "DUT1: \r\n" + this.richTextBox1.Text + "\r\n" +"DUT1 " + ((pcbastatus[0] == 1) ? "TEST RESULT :PASS" : "TEST RESULT :FAIL") + "\r\n";
                m = m + "DUT2: \r\n" + this.richTextBox2.Text + "\r\n" + "DUT2 " + ((pcbastatus[1] == 1) ? "TEST RESULT :PASS" : "TEST RESULT :FAIL") + "\r\n";
                m = m + "DUT3: \r\n" + this.richTextBox3.Text + "\r\n" + "DUT3 " + ((pcbastatus[2] == 1) ? "TEST RESULT :PASS" : "TEST RESULT :FAIL") + "\r\n";
                m = m + "DUT4: \r\n" + this.richTextBox4.Text + "\r\n" + "DUT4 " + ((pcbastatus[3] == 1) ? "TEST RESULT :PASS" : "TEST RESULT :FAIL") + "\r\n";
                m = m + "DUT5: \r\n" + this.richTextBox5.Text + "\r\n" + "DUT5 " + ((pcbastatus[4] == 1) ? "TEST RESULT :PASS" : "TEST RESULT :FAIL") + "\r\n";
                savelog(m);
            
            });





            });
           
 

         

    


        }

        public void savelog(string m) {

            string strlog = DateTime.Now.ToString() + ":\r\n";

            strlog = strlog + m;

            File.AppendAllText("testlog.txt", strlog);





        }

        public void realyset(int sp) {
            realycleaar();
            relay1.set_sing_relay((byte)sp,1);

        }

        public void realycleaar() {


            relay1.set_relay(00, 00);
        }

    }
}
