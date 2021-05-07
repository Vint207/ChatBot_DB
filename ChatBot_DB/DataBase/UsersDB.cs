using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class UsersDB
    {

        public Guid TableId { get; set; }


        public void CreateItem(User user)
        {
            SqlCommand query = new($"INSERT INTO [{TableId}] VALUES" +
                $"('{user.Name}'," +
                $"'{user.Password}'," +
                $"'{user.Mail}'," +
                $"{user.Money}," +
                $"{user.LastTransaction}," +
                $"'{user.UserID}'," +
                $"'{user.ArchiveId}'," +
                $"'{user.BinId}')");

            QueryDB.ExecuteNonQuery(query);
        }

        public void UpdateItem(User user)
        {
            SqlCommand query = new($"UPDATE [{TableId}] SET " +
                $"Name='{user.Name}'," +
                $"Password='{user.Password}'," +
                $"Mail='{user.Mail}'," +
                $"Money='{user.Money}'," +
                $"LastTransaction='{user.LastTransaction}'," +
                $"UserID='{user.UserID}'" +
                $"WHERE UserID='{user.UserID}'");

            QueryDB.ExecuteNonQuery(query);
        }

        public User ReadItem(User user)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE Mail='{user.Mail}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            if (!reader.HasRows) { return null; }
      

            while (reader.Read())
            {
                user.Name = (string)reader["Name"];
                user.Password = (string)reader["Password"];
                user.Mail = (string)reader["Mail"];
                user.Money = (double)reader["Money"];
                user.LastTransaction = (double)reader["LastTransaction"];
                user.UserID = (Guid)reader["UserID"];
                user.ArchiveId = (Guid)reader["ArchiveId"];
                user.BinId = (Guid)reader["BinId"];
            }
            return user;
        }

        public void DeleteItem(User user)
        {
            SqlCommand query = new($"DELETE [{TableId}] WHERE Mail='{user.Mail}'");
            QueryDB.ExecuteNonQuery(query);
        }

        public List<User> ReadAllItems()
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}]");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            List<User> users = new();

            while (reader.Read())
            {
                users.Add(new()
                {
                    Name = (string)reader["Name"],
                    Password = (string)reader["Password"],
                    Mail = (string)reader["Mail"],
                    Money = (double)reader["Money"],
                    LastTransaction = (double)reader["LastTransaction"],
                    UserID = (Guid)reader["UserID"],
                    ArchiveId = (Guid)reader["ArchiveId"],
                    BinId = (Guid)reader["BinId"]
                });
            }
            return users;
        }

        public void GetAllItemsInfo()
        {
            List<User> items = new();

            foreach (var item in items)
            { item?.GetInfo(); }
        }

        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"[Name] nvarchar(25) DEFAULT 'Гость' CHECK([Name] != '')," +
                                   $"[Password] nvarchar(15) null," +
                                   $"Mail nvarchar(35) null," +
                                   $"[Money] float DEFAULT 0 CHECK([Money] >= 0)," +
                                   $"LastTransaction float DEFAULT 0 CHECK(LastTransaction >= 0)," +
                                   $"UserID uniqueidentifier null," +
                                   $"ArchiveId uniqueidentifier null," +
                                   $"BinId uniqueidentifier null" +
                                   $")");

            QueryDB.ExecuteNonQuery(query);
        }

        public void DropTable()
        {
            SqlCommand query = new($"DROP TABLE [{TableId}]");
            QueryDB.ExecuteNonQuery(query);
        }
    }
}
