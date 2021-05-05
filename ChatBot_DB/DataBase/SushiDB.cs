using Microsoft.Data.SqlClient;
using System;

namespace ChatBot_DB
{
    class SushiDB
    {

        string _server = "Server=DESKTOP-J321LBP;Database=ChatBot;Trusted_Connection=True;";

        public void CreateItem(Sushi sushi)
        {
            SqlCommand command = new($"INSERT INTO Sushis VALUES" +                                   
                                     $"('{sushi.Name}'," +
                                     $"{sushi.Price}," +
                                     $"'{sushi.ID}')");

            if (ReadItem(sushi) == null)
            { ExecuteNonQuery(command); }
        }

        public void UpdateItem(Sushi sushi)
        {            
            SqlCommand command = new($"UPDATE Sushis SET " +
                                     $"Name='{sushi.Name}'," +
                                     $"Price={sushi.Price}" +
                                     $"WHERE ID='{sushi.ID}'");
            ExecuteNonQuery(command);
        }

        public Sushi ReadItem(Sushi sushi)
        {
            SqlCommand command = new($"SELECT * FROM Sushis WHERE Name='{sushi.Name}'");

            try
            {
                using SqlConnection connection = new(_server);
                connection.Open();
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sushi.ID = (Guid)reader["ID"];
                    sushi.Name = (string)reader["Name"];
                    sushi.Price = (int)reader["Price"];
                }
                if (!reader.HasRows) { return null; }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return sushi;
        }

        public void DeleteItem(Sushi sushi)
        {
            SqlCommand command = new($"DELETE Sushis WHERE Name='{sushi.Name}'");
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
