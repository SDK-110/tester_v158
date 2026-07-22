using MySql.Data.MySqlClient;
using System.Data;

namespace testapp.mylib
{
    /// <summary>
    /// Simple MySQL wrapper using MySql.Data.MySqlClient.
    /// Replaces the legacy SimpleMysql.Mysql API.
    /// </summary>
    public class Mysql : System.IDisposable
    {
        private readonly string _connectionString;

        public Mysql(string server, string database, string username, string userpassword)
        {
            _connectionString = $"server={server};database={database};uid={username};pwd={userpassword};";
        }

        public Mysql(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Query method. Returns DataTable
        /// </summary>
        public DataTable Query(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Non Query method. Use for INSERT, UPDATE, DELETE. Returns affected rows.
        /// </summary>
        public int ExecNonQuery(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
