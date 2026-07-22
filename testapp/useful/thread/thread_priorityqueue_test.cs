using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.thread;

namespace priorityQueue_test
{
 
   
    public partial class thread_priorityqueue_test : Form
    {
        Timer serverTimer = new Timer();
        ThreadSafePriorityQueue<portywork> _queue = new ThreadSafePriorityQueue<portywork>();
        volatile int flog = 0;
        public thread_priorityqueue_test()
        {
            InitializeComponent();
          
            serverTimer.Interval = 2000; // 设置时间间隔为5秒
            serverTimer.Tick += OnTimedEvent;
            serverTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            if (flog == 1) return;
           

              
                Task.Factory.StartNew(() => {
                 
                    while (_queue.TryDequeue(out portywork tmp))
                    {
                        flog = 1;
                        string  p = tmp.workname();
                        this.Invoke(new Action(() =>
                        {

                            this.richTextBox1.Text += tmp.porty.ToString() + " " + p + "\r\n";

                        }));


                    }

                    flog = 0;

                });
             
            }
          
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            _queue.Enqueue(new portywork() { porty = i++, workname = () => { System.Threading.Thread.Sleep(1000); return "1111111111"; } }, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _queue.Enqueue(new portywork() { porty = i2++, workname = () => { System.Threading.Thread.Sleep(1000); return "22222222222"; } }, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _queue.Enqueue(new portywork() { porty = i3++, workname = () => { System.Threading.Thread.Sleep(1000); return "333333333333"; } },3);
        }

        int i,i2,i3 = 0;
    }


    public class portywork {

       public int porty;
        public do_something workname;
    
    
    }
    public delegate string do_something();
}
