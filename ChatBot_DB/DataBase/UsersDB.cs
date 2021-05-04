using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace Chat_Bot
{
    public class UsersDB
    {

        string _server = "Server=DESKTOP-J321LBP;Database=ChatBot;Trusted_Connection=True;";


        public async Task CreateItem(UserMiddle user)
        {
            SqlCommand command = new($"INSERT INTO Users VALUES" +
                $"('{user.Name}'," +
                $" '{user.Password}', " +
                $"'{user.Mail}', " +
                $"'{user.Money}', " +
                $"'{user.LastTransaction}'," +
                $" '{new Guid()}')");

            await ExecuteNonQueryAsync(command);
        }

        public async Task UpdateItem(UserMiddle user)
        {
            SqlCommand command = new($"UPDATE Users SET " +
                $"Name='{user.Name}'" +
                $"Password='{user.Name}'" +
                $"Mail='{user.Mail}'" +
                $"Money='{user.Money}'" +
                $"LastTransaction='{user.LastTransaction}'" +
                $"ID='{user.ID}'" +
                $"WHERE Mail='{user.Mail}' AND Password='{user.Password}'");

            await ExecuteNonQueryAsync(command);
        }

        async Task ExecuteNonQueryAsync(SqlCommand command)
        {
            try
            {
                using SqlConnection connection = new(_server);
                await connection.OpenAsync();
                command.Connection = connection;
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
