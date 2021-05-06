using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    class SushisDB
    {

        public Guid TableId { get; set; }

        public void CreateItem(Sushi sushi)
        {
            if (ReadItem(sushi) == null)
            {
                SqlCommand query = new($"INSERT INTO [{TableId}] VALUES" +
                                         $"('{sushi.Name}'," +
                                         $"{sushi.Price}," +
                                         $"'{sushi.ID}')");

                QueryDB.ExecuteNonQuery(query);
            }
        }

        public void UpdateItem(Sushi sushi)
        {
            SqlCommand query = new($"UPDATE [{TableId}] SET " +
                                     $"Name='{sushi.Name}'," +
                                     $"Price={sushi.Price}" +
                                     $"WHERE ID='{sushi.ID}'");
            QueryDB.ExecuteNonQuery(query);
        }

        public Sushi ReadItem(Sushi sushi)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE Name='{sushi.Name}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            Sushi tempSushi = null;

            while (reader.Read())
            {
                tempSushi.Name = (string)reader["Name"];
                tempSushi.Price = (double)reader["Price"];
                tempSushi.ID = (Guid)reader["ID"];
            }
            return tempSushi;
        }

        public void DeleteItem(Sushi sushi)
        {
            SqlCommand query = new($"DELETE [{TableId}] WHERE Name='{sushi.Name}'");
            QueryDB.ExecuteNonQuery(query);
        }

        public List<Sushi> ReadAllItems()
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}]");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            List<Sushi> sushis = new();

            while (reader.Read())
            {
                sushis.Add(new()
                {
                    Name = (string)reader["Name"],
                    Price = (double)reader["Price"],
                    ID = (Guid)reader["ID"]
                });
            }
            return sushis;
        }

        public void GetAllItemsInfo()
        {
            foreach (var item in ReadAllItems())
            { item?.GetInfo(); }
        }

        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"[Name] nvarchar(25) UNIQUE CHECK([Name] != '')," +
                                   $"Price float DEFAULT 100 CHECK(Price >= 0)," +
                                   $"ID uniqueidentifier" +
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
