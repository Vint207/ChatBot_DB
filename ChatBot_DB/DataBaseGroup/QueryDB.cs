using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ChatBot_DB
{
    class QueryDB
    {

        static string _server = "Server=BEST-KOMP;Database=ChatBot;Trusted_Connection=True;";

        public static SqlDataReader ReadItem(SqlCommand query)
        {
            try
            {
                SqlConnection connection = new(_server);
                connection.Open();
                query.Connection = connection;
                SqlDataReader reader = query.ExecuteReader(CommandBehavior.CloseConnection);              
                return reader;
            }
            catch (SqlException ex)
            {
                Log.LogInfo($"Попытка выполнить запрос {query} к базе. Исключение:", ex);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return null;
            }         
        }

        public static void ExecuteNonQuery(SqlCommand query)
        {
            try
            {
                using SqlConnection connection = new(_server);
                connection.Open();
                query.Connection = connection;
                query.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                Log.LogInfo($"Попытка выполнить запрос {query} к базе. Исключение:", ex);
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
