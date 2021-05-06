﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    class RacksDB
    {

        public Guid TableId { get; set; }
        public Guid SushiTableId { get; set; }

        public void CreateItem(Rack rack)
        {
            if (ReadItem(rack) == null)
            {
                SqlCommand query = new($"INSERT INTO {TableId} VALUES" +
                                       $"('{rack.Name}'," +
                                       $"{1})");

                QueryDB.ExecuteNonQuery(query);
            }
            rack.Amount++;
            UpdateItem(rack);
        }

        public Rack ReadItem(Rack rack)
        {
            SqlCommand query = new($"SELECT * FROM {TableId} WHERE Name='{rack.Name}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            while (reader.Read())
            {
                rack.Name = (string)reader["Name"];
                rack.Amount = (int)reader["Amount"];
            }
            return rack;
        }

        public void UpdateItem(Rack rack)
        {
            SqlCommand query = new($"UPDATE {TableId} SET " +
                                   $"Name='{rack.Name}'," +
                                   $"Amount={rack.Amount}" +
                                   $"WHERE Name='{rack.Name}'");

            QueryDB.ExecuteNonQuery(query);
        }

        public void DeleteItem(Rack rack)
        {
            Rack rackDB = ReadItem(rack);

            if (rackDB != null)
            {
                if (rackDB.Amount > 0)
                {
                    rackDB.Amount--;
                    UpdateItem(rackDB);
                }
                SqlCommand query = new($"DELETE {TableId} WHERE Name='{rack.Name}'");
                QueryDB.ExecuteNonQuery(query);
            }
        }

        public List<Rack> ReadAllItems()
        {
            SqlCommand query = new($"SELECT * FROM {TableId}");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            List<Rack> racks = new();

            while (reader.Read())
            { racks.Add(new() { Name = (string)reader["Name"], Amount = (int)reader["Amount"] }); }

            return racks;
        }

        public void GetAllRacksInfo()
        {
            foreach (var item in ReadAllItems())
            { item?.GetInfo(); }
        }

        public double GetPrice()
        {
            SushisDB sushiDB = new();
            double result = 0;

            foreach (var item in ReadAllItems())
            { result += sushiDB.ReadItem(new() { Name = item.Name }).Price * item.Amount; }

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