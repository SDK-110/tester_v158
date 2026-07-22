using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
namespace testapp
{
  public static   class tttt
    {

  
            static string test1 = " test my loop  test item  oo than  ggg ".Substring(0, 32);
            static string test2 = " test my loop  test item  oo than  ggg ".Substring(4, 16);

            public static uint GetStableHashCode(this string str)
            {
                unchecked
                {

                    uint hash1 = 2818;
                    uint hash2 = hash1;

                    for (int i = 0; i < str.Length && str[i] != '\0'; i += 2)
                    {
                        hash1 = ((hash1 << 5) + hash1) ^ str[i];
                        if (i == str.Length - 1 || str[i + 1] == '\0')
                            break;
                        hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                    }

                    return hash1 + (hash2 * 1989091020);
                }
            }
            public static string getcpinf()
            {



                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Processor");
                string rt = "";
                foreach (var m in searcher.Get())
                {


                    rt = m["ProcessorId"].ToString();

                    break;

                }

                return rt;




            }


            public static string getyingpan()
            {

                ManagementClass mo = new ManagementClass("Win32_PhysicalMedia");

                ManagementObjectCollection mc = mo.GetInstances();

                string t = "";
                foreach (var moo in mc)
                {
                    t = moo.Properties["SerialNumber"].Value.ToString();

                    break;
                }

                return t;


            }

            public static string zhuban()
            {


                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
                string rt = "";
                foreach (var m in searcher.Get())
                {


                    rt = m["SerialNumber"].ToString();

                    break;

                }

                return rt;




            }









            public static string EncryptAES(string text)
            {
                var sourceBytes = System.Text.Encoding.UTF8.GetBytes(text);
                var aes = new System.Security.Cryptography.RijndaelManaged();
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                aes.Key = System.Text.Encoding.UTF8.GetBytes(test1);
                aes.IV = System.Text.Encoding.UTF8.GetBytes(test2);
                var transform = aes.CreateEncryptor();
                return System.Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
            }

            public static string DecryptAES(string text)
            {
                var encryptBytes = System.Convert.FromBase64String(text);
                var aes = new System.Security.Cryptography.RijndaelManaged();
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                aes.Key = System.Text.Encoding.UTF8.GetBytes(test1);
                aes.IV = System.Text.Encoding.UTF8.GetBytes(test2);
                var transform = aes.CreateDecryptor();
                return System.Text.Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
            }
        }


    
}
