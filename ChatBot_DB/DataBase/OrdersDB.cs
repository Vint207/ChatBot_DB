using Microsoft.Data.SqlClient;
using System;

namespace ChatBot_DB
{
    public class OrdersDB
    {

        public Guid TableId { get; set; }



        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"[Name] nvarchar(25) UNIQUE CHECK([Name] != '')," +
                                   $"OpenDate DateTime," +
                                   $"CloseDate DateTime," +
                                   $"Paid bit," +
                                   $"Closed bit" +
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
