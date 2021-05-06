using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot_DB
{
    public class OrderBase : ICRUD<Order, ProtoUser>
    {
        List<Order> _itemList;
        public event BaseChangedEvent<Order, ProtoUser> baseChangedEvent;

        public OrderBase() { _itemList = new(); }

        public bool AddItem(Order order, ProtoUser ProtoUser)
        {
            if (order != null)
            {
                _itemList.Add(order);
                baseChangedEvent?.Invoke(order, ProtoUser);
                return true;
            }
            return false;
        }

        public bool DeleteItem(Order order, ProtoUser ProtoUser)
        {
            baseChangedEvent?.Invoke(order, ProtoUser);
            _itemList.Remove(order);
            return true;
        }

        public Order GetItem(Order order, ProtoUser ProtoUser)
        {
            baseChangedEvent?.Invoke(order, ProtoUser);

            foreach (var item in _itemList)
            {
                if (item.Id.Equals(order?.Id))
                { return item; }
            }
            return null;
        }

        public void AddOrder(ProtoUser ProtoUser)
        {
            Console.Clear();

            //if (ProtoUser.Bin.itemList.Count > 0)
            //{
            //    Order order = new() { Price = ProtoUser.Bin.Price };

            //    foreach (var item in ProtoUser.Bin.itemList)
            //    {
            //        if (item.Value > 0)
            //        { order.itemList.Add(item.Key, item.Value); }
            //    }
            //    AddItem(order, ProtoUser);

            //    Console.WriteLine("Заказ сформирован");
            //    ProtoUser.Bin.EmptyBin(ProtoUser);
            //    Console.ReadKey();
            //    return;
            //}
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
