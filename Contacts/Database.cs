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
    public enum DatabaseStatus
    {
        UserExists,
        UserNotExists,
        WrongPassword,
        Success,
        Failure
    }

    public class Database
    {
        protected string path { get; set; } = "";
        protected string currentTable { get; set; } = "";
        public Database(string path = "../../Database/database.db")
        {
            this.path = path;
        }

        protected bool tableExists(string table)
        {
            bool result = false;
            using (var connection = openConnection())
            {
                string query = $"SELECT count(*) from sqlite_master where type='table' And name='user_{table}';";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                result = Convert.ToInt32(command.ExecuteScalar()) == 1;
            }
            return result;
        }


        public DatabaseStatus Login(string username, string password)
        {
            if (!tableExists(username))
            {
                return DatabaseStatus.UserNotExists;
            }
            string rightPassword = "";
            using (SQLiteConnection connection = openConnection())
            {
                string query = $"select phone from user_{username} where id=0; ";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                rightPassword = command.ExecuteScalar().ToString();
            }

            if (rightPassword != password)
            {
                return DatabaseStatus.WrongPassword;
            }
            else
            {
                this.currentTable = username;
                return DatabaseStatus.Success;
            }


        }

        public DatabaseStatus Register(string username, string password)
        {
            int result = 0;
            if (tableExists(username))
            {
                return DatabaseStatus.UserExists;
            }
            using (var connection = openConnection())
            {
                string query = $"CREATE TABLE user_{username} (id integer primary key AUTOINCREMENT, name text, phone text);";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                query = $"INSERT INTO user_{username} (id,phone) VALUES(0,'{password}')";
                command = new SQLiteCommand(query, connection);
                result = command.ExecuteNonQuery();
            }
            return result > 0 ? DatabaseStatus.Success : DatabaseStatus.Failure;
        }

        protected SQLiteConnection openConnection()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + this.path);
            conn.Open();
            return conn;
        }

        public List<Contact> AcquireContacts()
        {
            List<Contact> list = new List<Contact>();
            using (SQLiteConnection connection = openConnection())
            {
                string sql = $"select * from user_{this.currentTable} where id>0;";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Contact()
                    {
                        ID= Convert.ToInt32(reader["id"]),
                        Name = reader["name"].ToString(),
                        Phone = reader["phone"].ToString()
                    });
                }
            }
            return list;
        }

        public DatabaseStatus AddContact(Contact contact)
        {
            int result = 0;
            using (SQLiteConnection connection = openConnection())
            {
                string sql = $"INSERT INTO user_{this.currentTable} (name,phone) VALUES('{contact.Name}','{contact.Phone}');";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                result = command.ExecuteNonQuery();
            }
            return result > 0 ? DatabaseStatus.Success : DatabaseStatus.Failure;
        }

        public DatabaseStatus RemoveContact(Contact contact)
        {
            return RemoveContact(contact.Name);
        }
        public DatabaseStatus RemoveContact(string name="", string phone = "")
        {
            int result = 0;
            using (SQLiteConnection connection = openConnection())
            {
                string sql = $"DELETE FROM user_{this.currentTable} where (name='{name}' or phone='{phone}');";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                result = command.ExecuteNonQuery();
            }
            return result > 0 ? DatabaseStatus.Success : DatabaseStatus.Failure;
        }
        public DatabaseStatus UpdateContact(Contact oldContact, Contact newContact)
        {
            int result = 0;
            using (SQLiteConnection connection = openConnection())
            {
                string sql = $"UPDATE user_{this.currentTable} set name='{newContact.Name}', phone='{newContact.Phone}' where (name='{oldContact.Name}' or phone={oldContact.Phone}'); ";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                result = command.ExecuteNonQuery();
            }
            return result > 0 ? DatabaseStatus.Success : DatabaseStatus.Failure;
        }

    }




}
