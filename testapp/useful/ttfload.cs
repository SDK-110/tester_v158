using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  testapp.useful
{

    public static class IconfontHelper
    {
        private static System.Drawing.Text.PrivateFontCollection pfcc;

        public static System.Drawing.Text.PrivateFontCollection PFCC
        {
            get { return pfcc ?? LoadFont(); }
            set { pfcc = value; }
        }
        public static bool JzFont { get; private set; } = false;
        public static System.Drawing.Text.PrivateFontCollection LoadFont()
        {
            if (!JzFont)
            {
                pfcc = new System.Drawing.Text.PrivateFontCollection();
                pfcc.AddFontFile(Environment.CurrentDirectory + "/Material Design Icons.ttf");

                JzFont = true;
            }
            return pfcc;
        }
    }
}
