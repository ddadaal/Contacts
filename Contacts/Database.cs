using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace Contacts
{
    public class DatabaseException : Exception
    {
    }

    public class UserExistsException : DatabaseException
    {
        private string Username { get; set; } = "";
        public UserExistsException(string username)
        {
            this.Username = username;
        }

    }
    public class UserNotExistsException : DatabaseException
    {
        private string Username { get; set; } = "";
        public UserNotExistsException(string username)
        {
            this.Username = username;
        }

    }

    public class WrongPasswordException : DatabaseException
    {

    }


    public class Database
    {
        protected string path { get; set; } = "";
        protected string currentTable { get; set; } = "";
        protected SQLiteConnection connection = null;
        public Database(string path="/Database/database.db")
        {
            this.path = path;
        }

        protected bool tableExists(string table)
        {
            bool result = false;
            using (var connection = openConnection())
            {
                connection.Open();
                string query = $"SELECT count(*) FROM sqlite_master WHERE type='table' AND name='{this.currentTable}';";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                result = Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
            return result;
        }

        public void Login(string username, string password)
        {
            if (!tableExists(username))
            {
                throw new UserNotExistsException(username);
            }
            var connection = openConnection();

            string query = $"select password from {username}; ";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            string rightPassword = command.ExecuteScalar().ToString();
            if (rightPassword != password)
            {
                connection.Close();
                throw new WrongPasswordException();
            }
            else
            {
                this.currentTable = username;
                connection.Close();
            }


        }

        public bool Register(string username,string password)
        {
            int result = 0;
            if (tableExists(username))
            {
                throw new UserExistsException(username);
            }
            using (var connection = openConnection())
            {
                connection.Open();
                string query = $"CREATE TABLE {username}(id integer primary key, name text, phone text";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                result = command.ExecuteNonQuery();
            }
            return result > 0;
        }

        private SQLiteConnection openConnection()
        {
            return new SQLiteConnection ("Data Source=" + this.path);
        }

        public List<Contact> AcquireContacts()
        {
            List<Contact> list = new List<Contact>();
            using (SQLiteConnection connection = openConnection())
            {
                connection.Open();
                string sql = "select * from "+this.currentTable;
                SQLiteCommand command = new SQLiteCommand(sql,connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string phone = reader["phone"].ToString();
                    list.Add(new Contact()
                    {
                        Name = name,
                        Phone = phone
                    });
                }
            }
            return list;
        }
    }



    public class SQLiteDBHelper
    {
        private string connectionString = string.Empty;
        /// <summary> 
             /// 构造函数 
             /// </summary> 
             /// <param name="dbPath">SQLite数据库文件路径</param> 
        public SQLiteDBHelper(string dbPath)
        {
            this.connectionString = "Data Source=" + dbPath;
        }
        /// <summary> 
             /// 创建SQLite数据库文件 
             /// </summary> 
             /// <param name="dbPath">要创建的SQLite数据库文件路径</param> 
        public static void CreateDB(string dbPath)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE Demo";
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary> 
             /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
             /// </summary> 
             /// <param name="sql">要执行的增删改的SQL语句</param> 
             /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
             /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }
        /// <summary> 
             /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
             /// </summary> 
             /// <param name="sql">要执行的查询语句</param> 
             /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
             /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary> 
             /// 执行一个查询语句，返回一个包含查询结果的DataTable 
             /// </summary> 
             /// <param name="sql">要执行的查询语句</param> 
             /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
             /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
             /// 执行一个查询语句，返回查询结果的第一行第一列 
             /// </summary> 
             /// <param name="sql">要执行的查询语句</param> 
             /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
             /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
             /// 查询数据库中的所有数据类型信息 
             /// </summary> 
             /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns) 
                //{ 
                //  Console.WriteLine(column.ColumnName); 
                //} 
                return data;
            }
        }
    }
}
