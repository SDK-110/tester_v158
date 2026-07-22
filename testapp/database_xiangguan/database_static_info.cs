using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testapp.mylib;

namespace testapp
{
  static   class databases_static_info
    {
         static  string strial_number = "";
        static string  mysqlip="";
        static string dbname = "";
        static string upload_table = "";
        static string local_table = "";
        static string serial_number = "";
        static string save_sql_str = $"select * from `{local_table}` where `serial_number` = '{serial_number}'";


        public static void test() {

            testcase_dll.message_tran = (o) =>
            {
                strial_number = o as string;

                System.Windows.Forms.MessageBox.Show(strial_number);
                
            };
           
        }


        static void  logsave_database_local() {



            utility_func.instert_mysql_value("127.0.0.1", dbname, local_table, strial_number);

        }

        static void upload_logsave_server() {




        }
        

    }
}
