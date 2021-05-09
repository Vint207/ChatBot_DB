using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class ArchiveDB
    {

        public Guid TableId { get; set; }

        public void CreateItem(OrderDB order)
        {
            SqlCommand query = new($"INSERT INTO [{TableId}] VALUES" +
                                   $"(" +
                                   $"'{order.TableId}'," +
                                   $"'{order.OpenDate}'," +
                                   $"'{order.CloseDate}'," +
                                   $"{order.OrderPrice}," +
                                   $"'{order.Paid}'," +
                                   $"'{order.Closed}'" +
                                   $")");

            QueryDB.ExecuteNonQuery(query);
        }

        public OrderDB ReadItem(OrderDB order)

        {
            SqlCommand query = new($"SELECT * FROM [{TableId}] WHERE OrderID='{order.TableId}'");

            using SqlDataReader reader = QueryDB.ReadItem(query);

            if (!reader.HasRows) { return null; }

            OrderDB tempOrder = new();

            while (reader.Read())
            {
                tempOrder.TableId = (Guid)reader["OrderID"];
                tempOrder.OpenDate = (DateTime)reader["OpenDate"];
                tempOrder.CloseDate = (DateTime)reader["CloseDate"];
                tempOrder.OrderPrice = (double)reader["OrderPrice"];
                tempOrder.Paid = (bool)reader["Paid"];
                tempOrder.Closed = (bool)reader["Closed"];
            }
            return tempOrder;
        }

        public void UpdateItem(OrderDB order)
        {
            SqlCommand query = new($"UPDATE [{TableId}] SET " +
                                   $"OrderID='{order.TableId}'," +
                                   $"OpenDate='{order.OpenDate}'," +
                                   $"CloseDate='{order.CloseDate}'," +
                                   $"OrderPrice={order.OrderPrice}," +
                                   $"Paid='{order.Paid}'," +
                                   $"Closed='{order.Closed}'" +
                                   $"WHERE OrderID='{order.TableId}'");

            QueryDB.ExecuteNonQuery(query);
        }

        public void CreateTable(Guid id)
        {
            TableId = id;
            SqlCommand query = new($"CREATE TABLE [{TableId}]" +
                                   $"(" +
                                   $"OrderID uniqueidentifier," +
                                   $"OpenDate DateTime2," +
                                   $"CloseDate DateTime2," +
                                   $"OrderPrice float," +
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

        public void GetOrderInfo(Guid orderId)
        {
            OrderDB order = ReadItem(new() { TableId = orderId });

            List<Rack> items = order?.ReadAllItems();
            Console.Clear();

            if (items != null)
            {
                Console.WriteLine("Последний заказ:");

                foreach (var item in items)
                { item?.GetInfo(); }

                Console.WriteLine($"Откыт: {order.OpenDate}");

                if (order.CloseDate != default)
                { Console.WriteLine($"Закрыт: {order.CloseDate}"); }
                else
                { Console.WriteLine($"Не закрыт"); }

                if (order.Paid)
                { Console.WriteLine($"Оплачен"); }
                else 
                { Console.WriteLine($"Не оплачен"); }

                Console.ReadKey();
                return;
            }
            Console.WriteLine("Заказов нет");
            Console.ReadKey();
            return;
        }
    }
}
