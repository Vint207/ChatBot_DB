using Microsoft.Data.SqlClient;
using System;

namespace ChatBot_DB
{
    public class ArchiveDB
    {

        public Guid TableId { get; set; }

        public void CreateItem(OrderDB order)
        {
            SqlCommand query = new($"INSERT INTO [{TableId}] VALUES" +
                                   $"('" +
                                   $"{order.TableId}'," +
                                   $"{order.OpenDate}," +
                                   $"{order.CloseDate}," +
                                   $"{order.Paid}," +
                                   $"{order.Closed}" +        
                                   $")");

            QueryDB.ExecuteNonQuery(query);
        }


        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"OrderID uniqueidentifier," +
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
