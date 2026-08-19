using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

namespace testapp.mylib
{
  static public  class mul_thread_works
    {
       public static ConcurrentQueue<object> queue=new ConcurrentQueue<object>();




     public static  void do_background_task() {


            if (queue != null) {

                string[] item = new string[] { };



                object tmp_obj = null;
                while (queue.TryDequeue(out tmp_obj))
                {

                    string p = "";
                    foreach (var tp in (tmp_obj as string[])) {

                        p = p + tp + ",";
                    }



                    using (StreamWriter sw = new StreamWriter("d:/1.txt", true))
                    {
                        sw.Write(p.Substring(0, p.Length - 1) + "\n");
                    }
                   

                }



            }

         
     

        }

        public static void insert_work(string [] input) {

            queue.Enqueue(input);
        }
    }
}
