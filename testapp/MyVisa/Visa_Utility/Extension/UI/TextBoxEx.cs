using System.Windows.Forms;

namespace MyVISAInstrument.Mymodule.Extension.UI
{
    static class TextBoxEx
    {
        public static void SetSelect(this TextBoxBase textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
    }
}
