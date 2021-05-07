﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class RacksDB
    {

        public Guid TableId { get; set; }
        public Guid SushiTableId { get; set; }

        public void CreateItem(Rack rack)
        {
            Rack tempRack = ReadItem(rack);

            if (tempRack == null)
            {
                SqlCommand query = new($"INSERT INTO [{TableId}] VALUES" +
                                       $"('{rack.Name}'," +
                                       $"{rack.Amount})");

                QueryDB.ExecuteNonQuery(query);
                return;
            }
            tempRack.Amount++;
            UpdateItem(tempRack);
        }

        public Rack ReadItem(Rack rack)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE Name='{rack.Name}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            if (!reader.HasRows) { return null; }

            Rack tempRack = new();

            while (reader.Read())
            {
                tempRack.Name = (string)reader["Name"];
                tempRack.Amount = (int)reader["Amount"];
            }
            reader.Close();
            return tempRack;
        }

        public void UpdateItem(Rack rack)
        {
            SqlCommand query = new($"UPDATE [{TableId}] SET " +
                                   $"Name='{rack.Name}'," +
                                   $"Amount={rack.Amount}" +
                                   $"WHERE Name='{rack.Name}'");

            QueryDB.ExecuteNonQuery(query);
        }

        public void DeleteItem(Rack rack)
        {
            Rack tempRack = ReadItem(rack);

            if (tempRack != null)
            {
                if (tempRack.Amount > 0)
                {
                    tempRack.Amount--;
                    UpdateItem(tempRack);
                    return;
                }
                SqlCommand query = new($"DELETE [{TableId}] WHERE Name='{rack.Name}'");
                QueryDB.ExecuteNonQuery(query);
            }
        }

        public List<Rack> ReadAllItems()
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}]");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            List<Rack> racks = new();

            if (!reader.HasRows) { return null; }

            while (reader.Read())
            { racks.Add(new() { Name = (string)reader["Name"], Amount = (int)reader["Amount"] }); }

            return racks;
        }

        public void WriteAllItems(List<Rack> racks)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}]");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            foreach (var item in racks)
            { CreateItem(item); }
        }

        public void GetAllItemsInfo()
        {
            List<Rack> items = ReadAllItems();

            if (items!=null)
            {
                foreach (var item in items)
                { item?.GetInfo(); }              
                return;
            }
        }

        public double GetPrice()
        {
            SushisDB sushiDB = new() { TableId = SushiTableId };
            double result = 0;
            List<Rack> items = ReadAllItems();

            if (items!=null)
            {
                foreach (var item in items)
                { result += sushiDB.ReadItem(new() { Name = item.Name }).Price * item.Amount; }
            }
            return result;
        }

        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"[Name] nvarchar(25) UNIQUE CHECK([Name] != '')," +
                                   $"Amount int DEFAULT 0 CHECK(Amount >= 0)," +
                                   $"FOREIGN KEY([Name]) REFERENCES [{SushiTableId}]([Name]) " +
                                   $"ON UPDATE CASCADE " +
                                   $"ON DELETE CASCADE" +
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
