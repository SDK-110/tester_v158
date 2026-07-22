using System.Text.RegularExpressions;

namespace MyVISAInstrument.Mymodule.Extension
{
    public static class RegexEx
    {
        public static bool IsMatch(this string input,string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
    }
}