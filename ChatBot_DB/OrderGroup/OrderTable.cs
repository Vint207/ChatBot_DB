using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace ChatBot_DB
{
    public class OrderTable : RacksTable
    {

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public double OrderPrice { get; set; }
        public bool Paid { get; set; }
        public bool Closed { get; set; }

        public event EventEmail EventAction;

        public OrderTable()
        {
            EventAction += EmailService.SendEmail;
        }

        public void OpenOrder(User user)
        {
            Clear();
            BinDB bin = new() { TableId = user.BinId, SushiTableId = user.SushiTableID };

            if (bin.GetPrice() > 0)
            {
                TableId = user.UsersTableID;
                SushiTableId = user.SushiTableID;

                ArchiveTable archive = new() { TableId = user.ArchiveId };            
                UsersTable users = new() { TableId = user.UsersTableID };

                OpenDate = DateTime.Now;

                CreateTable(Guid.NewGuid());
                WriteAllItems(bin.ReadAllItems());
                OrderPrice = bin.GetPrice();

                archive.CreateItem(this);
                user.LastOrderId = TableId;

                users.UpdateItem(user);

                bin.ClearTable();

                WriteLine("Заказ сформирован");
                EventAction?.Invoke("Статус заказа", "Заказ сформирован");
                ReadKey();
                return;
            }
            WriteLine("Заказ не сформирован. Корзина пуста");
            ReadKey();
        }

        public void PayOrder(User user)
        {
            TableId = user.LastOrderId;
            SushiTableId = user.SushiTableID;

            Clear();

            if (user.LastOrderId != default)
            {
                ArchiveTable archive = new() { TableId = user.ArchiveId };             
                OrderTable order = archive.ReadItem(this);

                if (order!= null && !order.Paid)
                {
                    if (order.OrderPrice <= user.Money)
                    {
                        order.Paid = true;
                        archive.UpdateItem(order);
                        user.Money -= order.OrderPrice;

                        WriteLine($"Заказ оплачен");
                        EventAction?.Invoke("Статус заказа", "Заказ оплачен");
                        OrderDelay(user);
                        ReadKey();
                        return;
                    }
                    WriteLine($"На счету недостаточно средств");
                    ReadKey();
                    return;
                }
                WriteLine($"Последний заказ оплачен");
                ReadKey();
                return;
            }
            WriteLine($"Нет заказов");
            ReadKey();
        }

        async void OrderDelay(User user) =>
            await Task.Run(() => WaitTime(user));

        void WaitTime(User user)
        {
            Thread.Sleep(10000);
            CloseOrder(user);
        }

        public void CloseOrder(User user)
        {
            TableId = user.LastOrderId;
            SushiTableId = user.SushiTableID;

            ArchiveTable archive = new() { TableId = user.ArchiveId };
            OrderTable order = archive.ReadItem(this);

            order.CloseDate = DateTime.Now;
            order.Closed = true;

            archive.UpdateItem(order);

            EventAction?.Invoke("Статус заказа", "Заказ закрыт");
        }

        public void GetOrderInfo(User user)
        {
            TableId = user.LastOrderId;
            SushiTableId = user.SushiTableID;
            ArchiveTable archive = new() { TableId = user.ArchiveId };
            OrderTable orderInfo = archive.ReadItem(this);

            Clear();

            if (orderInfo != null)
            {
                List<Rack> items = ReadAllItems();

                WriteLine("Последний заказ:");

                foreach (var item in items)
                { item?.GetInfo(); }

                WriteLine($"Откыт: {orderInfo.OpenDate}");

                if (orderInfo.CloseDate != default)
                { WriteLine($"Закрыт: {orderInfo.CloseDate}"); }
                else 
                {
                    if (orderInfo.Paid)
                    { WriteLine($"Оплачен"); }
                    else
                    { WriteLine($"Не оплачен"); }

                    WriteLine($"Не закрыт"); 
                }
                ReadKey();
                return;
            }
            WriteLine("Заказов нет");
            ReadKey();
            return;
        }
    }
}
