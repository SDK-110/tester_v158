using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
// using Windows.UI.Xaml.Controls;

namespace testapp.duochuangti
{
    public partial class debug_form : DockContent
    {
        ConcurrentQueue<string> buf = new ConcurrentQueue<string>();
        static object lock_this = new object();
        static debug_form debug_f;
        private debug_form()
        {
            InitializeComponent();
        }

        public static debug_form GetDebug_f_instance() { 
        
        if(debug_f == null)debug_f = new debug_form(); ;

            return debug_f;
        
        }

        public void write_msg(string mssg) {
          
            buf.Enqueue(mssg);


        }

        public void clear() {
            lock (lock_this)
            {
                this.Invoke(new Action(() => {
                    this.richTextBox1.Clear();
                }));
            }
        
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
                // 检查 RichTextBox 的文本长度是否超出指定容量
                if (richTextBox1.TextLength > 5000)
                {
                    // 计算需要删除的字符数量
                    int excessChars = richTextBox1.TextLength - 2500;

                    // 删除文本框中的前 excessChars 个字符
                    richTextBox1.Text = richTextBox1.Text.Remove(0, excessChars);

                    // 将光标移到文本框末尾
                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();
                }
            }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string tmp="";
            if (buf.Count > 0)
            {

                for (int i = 0; i < buf.Count; i++)
                {

                    buf.TryDequeue(out tmp);

                    this.richTextBox1.AppendText(DateTime.Now.ToString() + ":" + "\n" + tmp + "\n");
                }

            }
        }

        private void debug_form_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.timer1.Interval = 100;
            this.timer1.Tick+= timer1_Tick;
            this.timer1.Enabled = true;
            

        }

        private void debug_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }
    }
}
