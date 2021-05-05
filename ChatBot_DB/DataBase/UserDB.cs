using Microsoft.Data.SqlClient;
using System;

namespace ChatBot_DB
{
    public class UserDB
    {

        string _server = "Server=DESKTOP-J321LBP;Database=ChatBot;Trusted_Connection=True;";

        public void CreateItem(UserMiddle user)
        {
            SqlCommand command = new($"INSERT INTO Users VALUES" +
                $"('{user.Name}'," +
                $" '{user.Password}', " +
                $"'{user.Mail}', " +
                $"'{user.Money}', " +
                $"'{user.LastTransaction}'," +
                $" '{new Guid()}')");

            ExecuteNonQuery(command);
        }

        public void UpdateItem(UserMiddle user)
        {
            SqlCommand command = new($"UPDATE Users SET " +
                $"Name='{user.Name}'," +
                $"Password='{user.Password}'," +
                $"Mail='{user.Mail}'," +
                $"Money='{user.Money}'," +
                $"LastTransaction='{user.LastTransaction}'," +
                $"ID='{user.ID}'" +
                $"WHERE ID='{user.ID}'");

            ExecuteNonQuery(command);
        }

        public UserMiddle ReadItem(UserMiddle user)
        {
            SqlCommand command = new($"SELECT * FROM Users WHERE Mail='{user.Mail}'");

            try
            {
                using SqlConnection connection = new(_server);
                connection.Open();
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.Name = (string)reader["Name"];
                    user.Password = (string)reader["Password"];
                    user.Mail = (string)reader["Mail"];
                    user.Money = (double)reader["Money"];
                    user.LastTransaction = (double)reader["LastTransaction"];
                }
                if (!reader.HasRows) { return null; }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return user;
        }

        public void DeleteItem(UserMiddle user)
        {
            SqlCommand command = new($"DELETE Users WHERE Mail='{user.Mail}'");

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
