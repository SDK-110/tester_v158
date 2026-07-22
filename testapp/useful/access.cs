public class AccessDatabase
{
    private OleDbConnection connection;

    public AccessDatabase()
    {
        connection = new OleDbConnection();
    }

    // 创建新的Access数据库并连接
    public void CreateDatabase(string dbName)
    {
        string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={dbName}.mdb;";

        connection.ConnectionString = connectionString;
        connection.Open();

        // 创建一个空的数据库文件
        ADOX.Catalog catalog = new ADOX.Catalog();
        catalog.Create(connectionString);
        catalog = null;
    }

    // 关闭连接并删除数据库文件
    public void DeleteDatabase(string dbName)
    {
        connection.Close();
        System.IO.File.Delete($"{dbName}.mdb");
    }

    // 创建新的表格
    public void CreateTable(string tableName, string[] columnNames, string[] dataTypes)
    {
        string query = $"CREATE TABLE {tableName} (";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += $"{columnNames[i]} {dataTypes[i]},";
        }

        query = query.TrimEnd(',');
        query += ")";

        OleDbCommand command = new OleDbCommand(query, connection);
        command.ExecuteNonQuery();
    }

    // 添加新的记录
    public void AddRecord(string tableName, string[] columnNames, object[] values)
    {
        string query = $"INSERT INTO {tableName} (";

        for (int i = 0; i < columnNames.Length; i++)
        {
            query += $"{columnNames[i]},";
        }

        query = query.TrimEnd(',');
        query += ") VALUES (";

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == null)
            {
                query += "NULL,";
            }
            else if (values[i] is string)
            {
                query += $"'{values[i]}',";
            }
            else
            {
                query += $"{values[i]},";
            }
        }

        query = query.TrimEnd(',');
        query += ")";

        OleDbCommand command = new OleDbCommand(query, connection);
        command.ExecuteNonQuery();
    }

    // 更新记录
    public void UpdateRecord(string tableName, string[] columnNames, object[] values, string whereClause)
    {
        string query = $"UPDATE {tableName} SET ";

        for (int i = 0; i < columnNames.Length; i++)
        {
            if (values[i] == null)
            {
                query += $"{columnNames[i]}=NULL,";
            }
            else if (values[i] is string)
            {
                query += $"{columnNames[i]}='{values[i]}',";
            }
            else
            {
                query += $"{columnNames[i]}={values[i]},";
            }
        }

        query = query.TrimEnd(',');
        query += $" WHERE {whereClause}";

        OleDbCommand command = new OleDbCommand(query, connection);
        command.ExecuteNonQuery();
    }

    // 删除记录
    public void DeleteRecord(string tableName, string whereClause)
    {
        string query = $"DELETE FROM {tableName} WHERE {whereClause}";

        OleDbCommand command = new OleDbCommand(query, connection);
        command.ExecuteNonQuery();
    }

    // 查询记录
    public OleDbDataReader QueryRecords(string tableName, string[] columnNames, string whereClause)
    {
        string query = $"SELECT ";

        if (columnNames.Length == 0)
        {
            query += "*";
        }
        else
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                query += $"{columnNames[i]},";
            }

            query = query.TrimEnd(',');
        }

        query += $" FROM {tableName}";

        if (!string.IsNullOrEmpty(whereClause))
        {
            query += $" WHERE {whereClause}";
        }

        OleDbCommand command = new OleDbCommand(query, connection);
        return command.ExecuteReader();
    }
}

/***
示例用法：

AccessDatabase database = new AccessDatabase();

// 创建新的数据库
database.CreateDatabase("test");

// 添加新的表格
string[] columnNames = { "ID", "Name", "Age" };
string[] dataTypes = { "INT", "VARCHAR(50)", "INT" };
database.CreateTable("Students", columnNames, dataTypes);

// 添加新的记录
string[] values = { "1", "John", "20" };
database.AddRecord("Students", columnNames, values);

// 查询记录
OleDbDataReader reader = database.QueryRecords("Students", columnNames, "");
while (reader.Read())
{
    Console.WriteLine($"ID={reader["ID"]}, Name={reader["Name"]}, Age={reader["Age"]}");

// 更新记录
string whereClause = "ID=1";
string[] updateColumnNames = { "Age" };
object[] updateValues = { 21 };
database.UpdateRecord("Students", updateColumnNames, updateValues, whereClause);

// 查询记录
reader = database.QueryRecords("Students", columnNames, "");
while (reader.Read())
{
Console.WriteLine($"ID={reader["ID"]}, Name={reader["Name"]}, Age={reader["Age"]}");
}

// 删除记录
whereClause = "ID=1";
database.DeleteRecord("Students", whereClause);

// 查询记录
reader = database.QueryRecords("Students", columnNames, "");
while (reader.Read())
{
Console.WriteLine($"ID={reader["ID"]}, Name={reader["Name"]}, Age={reader["Age"]}");
}

// 删除数据库文件并关闭连接
database.DeleteDatabase("test");

*/