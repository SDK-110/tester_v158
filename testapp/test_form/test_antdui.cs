using AntdUI;
using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.test_form
{
    public partial class test_antdui : AntdUI.Window
    {
        private bool setcolor;

        public test_antdui()
        {
            InitializeComponent();
         
        }

        private void test_antdui_Load(object sender, EventArgs e)
        {
    
           
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
           
                AntdUI.Config.Animation = true;
                AntdUI.Config.ShadowEnabled = true;
                AntdUI.Config.ShowInWindow = true;
                AntdUI.Config.ScrollBarHide = true;
                if (AntdUI.Config.TextRenderingHighQuality == true) return;
                AntdUI.Config.TextRenderingHighQuality = true;
                Refresh();
            
            if (setcolor)
            {
                var color = AntdUI.Style.Db.Primary;
                AntdUI.Config.IsDark = !AntdUI.Config.IsDark;
                Dark = AntdUI.Config.IsDark;
                AntdUI.Style.SetPrimary(color);
            }
            else
            {
                AntdUI.Config.IsDark = !AntdUI.Config.IsDark;
                Dark = AntdUI.Config.IsDark;
            }

         
            if (Dark)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
            }


          
             var   FloatButton = AntdUI.FloatButton.open(new AntdUI.FloatButton.Config(this, new AntdUI.FloatButton.ConfigBtn[] {
                            new AntdUI.FloatButton.ConfigBtn("id1", "SearchOutlined", true){
                                Tooltip = "搜索一下",
                                Type= AntdUI.TTypeMini.Primary
                            },
                            new AntdUI.FloatButton.ConfigBtn("id2"){
                                Badge = " ",
                                Tooltip = "笑死人",
                            },
                            new AntdUI.FloatButton.ConfigBtn("id3"){
                                Badge = "9",
                                Tooltip = "救救我"
                            },
                            new AntdUI.FloatButton.ConfigBtn("id4", "PoweroffOutlined", true){
                                Badge = "99+",
                                Tooltip = "没救了",
                                Round = false,
                                Type= AntdUI.TTypeMini.Primary
                            },
                              new AntdUI.FloatButton.ConfigBtn("id4", "PoweroffOutlined", true){
                                Badge = "99+",
                                Tooltip = "没救了",
                                Round = false,
                                Type= AntdUI.TTypeMini.Primary
                            }
                        }, btn =>
                        {
                            btn.Loading = true;
                            AntdUI.ITask.Run(() =>
                            {
                                System.Threading.Thread.Sleep(2000);
                                btn.Loading = false;
                            });
                            AntdUI.Message.info(this, "点击了：" + btn.Name, Font);
                        }));
          


            OnSizeChanged(e);
        }
    }
}
