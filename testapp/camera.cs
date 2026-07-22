using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace camer
{
    public partial class camera : Form
    {
        private VideoCaptureDevice videoSource;
        private FilterInfoCollection videoDevices;
        static camera __obj_inst__;
        public  string isok = "ng";
       static string prope = "";
       static string pic_path = "";
      private camera()
        {
            InitializeComponent();
        }

        public static camera get_inst(string prope, string pic_path)
        {

            if (__obj_inst__ == null)
            {

                __obj_inst__ = new camera();
            }
            camera.prope = prope;
            camera.pic_path = pic_path;
            return __obj_inst__;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null)
            {
               videoSourcePlayer1.Stop();
                videoSource.Stop();



            }

        }


        protected override void DefWndProc(ref Message ms)
        {


            switch (ms.Msg)
            {

                case WM_HIDE:
                    {


                        this.Hide();

                    }

                    break;

                case WM_SHOW:
                    {


                        this.Show();

                    }

                    break;
            }
            base.DefWndProc(ref ms);
        }

        public const int USER = 0x0400;
        public const int WM_HIDE = USER + 444;
        public const int WM_SHOW = USER + 555;

        public void set_show() {

            this.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (videoSource != null)
            {
                videoSourcePlayer1.Stop();
                videoSource.Stop();



            }
            isok = "ok";
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (videoSource != null)
            {
                videoSourcePlayer1.Stop();
                videoSource.Stop();



            }
            this.Hide();
             isok = "ng";
        }

        private void camera_Load(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.Location = new Point(0, 0);
            videoSource = new VideoCaptureDevice();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {

                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

                videoSource.NewFrame += VideoSource_NewFrame;

                videoSourcePlayer1.VideoSource = videoSource;
                
                // videoSourcePlayer1.NewFrame += VideoSourcePlayer1_NewFrame;
                videoSource.Start();
                videoSourcePlayer1.Start();





            }
            try
            {
                this.pictureBox1.Image = Image.FromFile(pic_path);
            }
            catch { }
            //this.Hide();
            this.label1.Text = prope;

            this.button1.Focus();
        }
    }
}
