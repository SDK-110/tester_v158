

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
namespace testapp.mylib
{
    public class mdbHelper
    {
        private OleDbConnection myConn;
        protected string test1;
        /// <summary>
        /// 初始化连接数据库
        /// </summary>
        /// <param name="address"></param>
        public mdbHelper(string address)
        {
            try
            {
                //创建一个 OleDbConnection对象
                string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source =" + address;
                myConn = new OleDbConnection(strCon);

                myConn.Open();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        /// <summary>
        /// 关闭数据连接
        /// </summary>
        public void CloseConnection()
        {
            myConn.Close();
        }
        /// <summary>
        /// 创建一个类型和datatable一致的空表
        /// </summary>
        /// <param name="tableName"></param>
        public void CreateTable(string tableName, DataTable dt)
        {
            try
            {
                string sql = "create table " + tableName;
                string tableAttribute = "";
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                // tableAttribute = tableAttribute + dt.Columns[i].ColumnName + " " + GetType(dt.Columns[i].DataType.ToString()) + ",";
                //}
                //sql = sql + "(Num AUTOINCREMENT," + tableAttribute + "CONSTRAINT table_PK PRIMARYKEY(Num));";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    tableAttribute = tableAttribute + dt.Columns[i].ColumnName + " " + GetType(dt.Columns[i].DataType.ToString());
                    if (i < dt.Columns.Count - 1)
                    {
                        tableAttribute = tableAttribute + ",";
                    }
                }
                sql = sql + "(" + tableAttribute + ");";
                OleDbCommand cmd = new OleDbCommand(sql, myConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 将datatable导入对应名字的表中
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="dt"></param>
        public void DatatableToMdb(string name, DataTable dt)
        {
            try
            {

                string strCom = string.Format("select * from {0}", name);
                OleDbDataAdapter da = new OleDbDataAdapter(strCom, myConn);
                //****
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);//这里的CommandBuilder对象一定不要忘了,一般就是写在DataAdapter定义的后面
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                DataSet midData = new DataSet();
                da.Fill(midData, name);
                foreach (DataRow dR in dt.Rows)
                {
                    DataRow dr = midData.Tables[name].NewRow();
                    dr.ItemArray = dR.ItemArray;//行复制
                    midData.Tables[name].Rows.Add(dr);
                }
                da.Update(midData, name);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DataTable DatatableToMdb(string name)
        {
            try
            {

                string strCom = string.Format("select * from {0}", name);
                OleDbDataAdapter da = new OleDbDataAdapter(strCom, myConn);
                //****
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);//这里的CommandBuilder对象一定不要忘了,一般就是写在DataAdapter定义的后面
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                DataSet midData = new DataSet();
                da.Fill(midData, name);
                return midData.Tables[0];

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取创建mdb表格的属性字段类型
        /// </summary>
        /// <param name="datatype"></param>
        /// <returns></returns>
        private string GetType(string datatype)
        {
            switch (datatype)//匹配类型选择
            {
                case "System.String":
                    return "TEXT(50)";
                case "System.DateTime":
                    return "DateTime";
                case "System.Double":
                    return "Double";
                case "System.Int32":
                case "System.Int16":
                case "System.Int64":
                    return "Int";
                default:
                    return "TEXT(50)";
            }
        }
        /// <summary>
        /// 创建Access数据库
        /// </summary>
        /// <param name="path">文件和文件路径</param>
        /// <returns>真为创建成功，假为创建失败或是文件已存在</returns>
        /// 
        /*
        public static bool CreateAccessDatabase(string path)
        {
            //如果文件存在反回假
            if (File.Exists(path))
            {
                MessageBox.Show("文件已存在！");
                return false;
            }
            try
            {
                //如果目录不存在，则创建目录
                string dirName = Path.GetDirectoryName(path);
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                //创建Catalog目录类

              ADOX.CatalogClass catalog = new ADOX.CatalogClass();
                string _connectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path
                       + ";";
                //根据联结字符串使用Jet数据库引擎创建数据库
                catalog.Create(_connectionStr);
                catalog = null;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw new Exception("数据库创建失败!");
            }
        }
    */
    }

}