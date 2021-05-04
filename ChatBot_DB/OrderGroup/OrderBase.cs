using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat_Bot
{
    public class OrderBase : ICRUD<Order, UserMiddle>
    {
        List<Order> _itemList;
        public event BaseChangedEvent<Order, UserMiddle> baseChangedEvent;

        public OrderBase() { _itemList = new(); }

        public bool AddItem(Order order, UserMiddle user)
        {
            if (order != null)
            {
                _itemList.Add(order);
                baseChangedEvent?.Invoke(order, user);
                return true;
            }
            return false;
        }

        public bool DeleteItem(Order order, UserMiddle user)
        {
            baseChangedEvent?.Invoke(order, user);
            _itemList.Remove(order);
            return true;
        }

        public Order GetItem(Order order, UserMiddle user)
        {
            baseChangedEvent?.Invoke(order, user);

            foreach (var item in _itemList)
            {
                if (item.Id.Equals(order?.Id))
                { return item; }
            }
            return null;
        }

        public void AddOrder(UserMiddle user)
        {
            Console.Clear();

            if (user.Bin.itemList.Count > 0)
            {
                Order order = new() { Price = user.Bin.Price };

                foreach (var item in user.Bin.itemList)
                {
                    if (item.Value > 0)
                    { order.itemList.Add(item.Key, item.Value); }
                }
                AddItem(order, user);

                Console.WriteLine("Заказ сформирован");
                user.Bin.EmptyBin(user);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Заказ не был сформирован");
            Console.ReadKey();
        }

        public Order GetLastOrder()
        {
            Console.Clear();

            if (_itemList.Count == 0)
            {
                Console.WriteLine("Список заказов пуст");
                Console.ReadKey();
                return null;
            }
            Order order = _itemList?.Last();
            return order;
        }
    }
}
