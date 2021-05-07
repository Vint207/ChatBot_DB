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

        public OrderDB ReadItem(OrderDB order)
        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE OrderID='{order.TableId}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            OrderDB tempOrder = null;

            while (reader.Read())
            {
                tempOrder.TableId = (Guid)reader["OrderID"];
                tempOrder.OpenDate = (DateTime)reader["OpenDate"];
                tempOrder.CloseDate = (DateTime)reader["CloseDate"];
                tempOrder.Paid = (bool)reader["Paid"];
                tempOrder.Closed = (bool)reader["Closed"];
            }
            return order;
        }

        public void UpdateItem(OrderDB order)
        {
            SqlCommand query = new($"UPDATE [{TableId}] SET" +
                                   $"(" +
                                   $"OrderID='{order.TableId}'," +
                                   $"OpenDate={order.OpenDate}," +
                                   $"CloseDate={order.CloseDate}," +
                                   $"Paid={order.Paid}," +
                                   $"Closed={order.Closed}" +
                                   $"WHERE OrderID='{order.TableId}'"+
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
