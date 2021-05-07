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
                $"'{user.BinId}'," +
                $"'{user.LastOrderId}'," +
                $"'{user.SushiTableID}'," +
                $"'{user.SushiRacksTableID}'," +
                $"'{user.UsersTableID}')");

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
                $"UserID='{user.UserID}'," +
                $"ArchiveId='{user.ArchiveId}'," +
                $"BinId='{user.BinId}'," +
                $"LastOrderId='{user.LastOrderId}'," +
                $"SushiTableID='{user.SushiTableID}'," +
                $"SushiRacksTableID='{user.SushiRacksTableID}'," +
                $"UsersTableID='{user.UsersTableID}'" +
                $"WHERE UserID='{user.UserID}'");

            QueryDB.ExecuteNonQuery(query);
        }

        public User ReadItem(User user)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE Mail='{user.Mail}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            if (!reader.HasRows) { return null; }

            User tempUser = new();

            while (reader.Read())
            {
                tempUser.Name = (string)reader["Name"];
                tempUser.Password = (string)reader["Password"];
                tempUser.Mail = (string)reader["Mail"];
                tempUser.Money = (double)reader["Money"];
                tempUser.LastTransaction = (double)reader["LastTransaction"];
                tempUser.UserID = (Guid)reader["UserID"];
                tempUser.ArchiveId = (Guid)reader["ArchiveId"];
                tempUser.BinId = (Guid)reader["BinId"];
                tempUser.LastOrderId = (Guid)reader["LastOrderId"];
                tempUser.SushiTableID = (Guid)reader["SushiTableID"];
                tempUser.SushiRacksTableID = (Guid)reader["SushiRacksTableID"];
                tempUser.UsersTableID = (Guid)reader["UsersTableID"];
            }
            reader.Close();
            return tempUser;
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
            List<User> items = ReadAllItems();

            if (items != null)
            {
                foreach (var item in items)
                { item?.GetInfo(); }
                return;
            }
            Console.WriteLine("Список пуст");
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
                                   $"BinId uniqueidentifier null," +
                                   $"LastOrderId uniqueidentifier null," +
                                   $"SushiTableID uniqueidentifier null," +
                                   $"SushiRacksTableID uniqueidentifier null," +
                                   $"UsersTableID uniqueidentifier null" +
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
