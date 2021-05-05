using Microsoft.Data.SqlClient;
using System;

namespace ChatBot_DB
{
    class SushiRackDB
    {

        string _server = "Server=DESKTOP-J321LBP;Database=ChatBot;Trusted_Connection=True;";

        public void CreateItem(SushiRack sushiRack)
        {
            SqlCommand command;

            if (ReadItem(sushiRack) is SushiRack tempSushi && tempSushi.Amount >= 0)
            {
                tempSushi.Amount += 1;
                UpdateItem(tempSushi);
                return;
            }
            else
            {
                command = new($"INSERT INTO SushiRacks VALUES" +
                              $"('{sushiRack.Name}'," +
                              $"'{1}')");
            }
            ExecuteNonQuery(command);
        }

        public void UpdateItem(SushiRack sushiRack)
        {
            SqlCommand command = new($"UPDATE SushiRacks SET " +
                                     $"Name='{sushiRack.Name}'," +
                                     $"Amount={sushiRack.Amount}" +
                                     $"WHERE Name='{sushiRack.Name}'");
            ExecuteNonQuery(command);
        }

        public SushiRack ReadItem(SushiRack sushiRack)
        {
            SqlCommand command = new($"SELECT * FROM SushiRacks WHERE Name='{sushiRack.Name}'");

            try
            {
                using SqlConnection connection = new(_server);
                connection.Open();
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sushiRack.Name = (string)reader["Name"];
                    sushiRack.Amount = (int)reader["Amount"];
                }
                if (!reader.HasRows) { return null; }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return sushiRack;
        }

        public void DeleteItem(SushiRack sushi)
        {
            SqlCommand command = new($"DELETE SushiRacks WHERE Name='{sushi.Name}'");
            ExecuteNonQuery(command);
        }

        void ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                using SqlConnection connection = new(_server);
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
