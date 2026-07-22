using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Org.BouncyCastle.Ocsp;
using System.Transactions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PCHMI;

namespace testapp.mylib
{
    class sqlite_handle
    {
      
        string databasefile = /*DateTime.Now.ToString("yyyyMMdd")*/"TestData" + ".db";
        string table_name = "TestData";
        string table_str = "DateTimeT varchar(20),BarCode varchar(20) ,FileName varchar(15),PF varchar(10),Mfrs varchar(10), Products varchar(10) ,PM tvarchar(10),OP tvarchar(10), TestInfo tvarchar(40)";
        SQLiteConnection con = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader reader = null;
        SQLiteTransaction transaction = null;
        public sqlite_handle(String table_name= "TestData")
        {
            if (false == (System.IO.File.Exists(/*DateTime.Now.ToString("yyyyMMdd") */ "TestData" + ".db"))) {

                SQLiteConnection.CreateFile(/*DateTime.Now.ToString("yyyyMMdd")*/"TestData" + ".db");
                databasefile = /*DateTime.Now.ToString("yyyyMMdd")*/"TestData" + ".db";
            }

            this.table_name = table_name;

            try {
                con = new SQLiteConnection("Data Source=" + databasefile);
                con.Open();
                cmd = new SQLiteCommand($"Select name from sqlite_master where type='table' and name ='{table_name}'", con);
                transaction = con.BeginTransaction();
                reader = cmd.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                using (SQLiteCommand createcmd = new SQLiteCommand($"CREATE TABLE {table_name} ({table_str})", con))
                                {

                                    createcmd.ExecuteNonQuery();
                                }

                            }

                reader.Close();
                cmd.Dispose();




                  



               

            }
            catch { }





            //using (SQLiteConnection con = new SQLiteConnection("Data Source=" + databasefile))
            //{
            //    con.Open();
            //    using (SQLiteCommand cmd = new SQLiteCommand($"Select name from sqlite_master where type='table' and name ='{table_name}'", con))
            //    {


            //        using (SQLiteDataReader reader = cmd.ExecuteReader())
            //        {


            //            if (!reader.HasRows)
            //            {
            //                using (SQLiteCommand createcmd = new SQLiteCommand($"CREATE TABLE {table_name} ({table_str})", con))
            //                {

            //                    createcmd.ExecuteNonQuery();
            //                }

            //            }

            //        }



            //    }



            //}
        }
        public  void InsertRecord(string DateTimeT=" "  , string BarCode=" "   , string FileName=" "   , string PF=" "  ,string  Mfrs=" "  ,string  Products=" "   ,string  PM=" "  ,string  OP= " "  ,string TestInfo=" ")
        {
            if (con != null) { 
            using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {table_name} (DateTimeT  ,BarCode   ,FileName  ,PF  ,Mfrs  , Products   ,PM  ,OP  , TestInfo) VALUES ('{DateTimeT}','{BarCode}','{FileName}','{PF}','{Mfrs}','{Products}','{PM}','{OP}','{TestInfo}')", con))
            {
                command.ExecuteNonQuery();

            }
            }

            // 连接到数据库
            // using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + this.databasefile))
            // {
            //    connection.Open();

            //     // 插入一条记录到表A
            //     using (SQLiteCommand command = new SQLiteCommand($"INSERT INTO {table_name} (BarCode,FileName) VALUES ('1111111111','22222222222')", connection))
            //     {
            //         command.ExecuteNonQuery();

            //     }

            //    connection.Close();
            //}
        }
        public void commit() {
            try
            {

                transaction.Commit();

            }
            catch { }
       
        }

        public  void DeleteRecord()
        {
           

            // 连接到数据库
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + this.databasefile))
            {
                connection.Open();

                // 删除表A中Name为'John Doe'的记录
                using (SQLiteCommand command = new SQLiteCommand("DELETE FROM A WHERE Name = 'John Doe'", connection))
                {
                    command.ExecuteNonQuery();
                  
                }

                connection.Close();
            }
        }

         ~sqlite_handle() {
            // if(cmd!=null) cmd.Dispose();
            try {
                if (transaction != null) transaction.Dispose();
                if (con != null) con.Dispose();
            } catch { }
          
        }
    }
}
