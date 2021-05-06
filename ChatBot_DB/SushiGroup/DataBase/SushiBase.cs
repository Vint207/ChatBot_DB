using System;
using System.Collections.Generic;
using static System.Console;

namespace ChatBot_DB
{
    public class SushiBase : ICRUD<Sushi, ProtoUser>
    {

        public Dictionary<Sushi, int> itemList;
        public event BaseChangedEvent<Sushi, ProtoUser> baseChangedEvent;

        public SushiBase()
        {
            //itemList = new()
            //{
            //    { new Sushi("Сяке-Маке", 100), 99 },
            //    { new Sushi("Филадельфия", 100), 99 },
            //    { new Sushi("Суши-Кавасаки", 100), 99 },
            //    { new Sushi("Хонда-Ролл", 100), 99 },
            //    { new Sushi("Фукусима-Глоу", 100), 99 },
            //    { new Sushi("Гуро-Харакири", 100), 99 },
            //    { new Sushi("Субару-Импреза", 99999), 99 },
            //};
        }

        public bool AddItem(Sushi sushi, ProtoUser ProtoUser)
        {
            foreach (var item in itemList)
            {
                if (item.Key.Name.Equals(sushi?.Name))
                {
                    itemList[item.Key]++;
                    return true;
                }
            }
            if (sushi != null)
            {
                itemList.Add(sushi, 1);
                baseChangedEvent?.Invoke(sushi, ProtoUser);
                return true;
            }
            return false;
        }

        public bool DeleteItem(Sushi sushi, ProtoUser ProtoUser)
        {
            foreach (var item in itemList)
            {
                if (item.Key.Name.Equals(sushi?.Name))
                {
                    itemList[item.Key]--;
                    baseChangedEvent?.Invoke(sushi, ProtoUser);
                    return true;
                }
            }
            return false;
        }
        
        public Sushi GetItem(Sushi sushi, ProtoUser ProtoUser)
        {
            foreach (var item in itemList)
            {
                if (item.Key.Name.Equals(sushi?.Name))
                {
                    baseChangedEvent?.Invoke(sushi, ProtoUser);
                    return item.Key;
                }
            }
            WriteLine("Таких суши нет, попробуй другие");
            return null;
        }

        public virtual void GetAllItemsInfo(ProtoUser guest)
        {
            Clear();
            WriteLine("Список суши:");

            GetItemsInfo(guest);

            ReadKey();
        }

        protected bool GetItemsInfo(ProtoUser guest)
        {
            foreach (var item in itemList)
            {
                if (item.Value > 0)
                { item.Key.GetInfo(); }
            }
            baseChangedEvent?.Invoke(null, guest);

            return true;
        }

        public List<string> GetListItems(ProtoUser guest)
        {
            List<string> sushiList = new();

            foreach (var item in itemList)
            {
                if (item.Value > 0)
                { sushiList.Add($" {item.Key.Name}. Цена {item.Key.Price} р. Количество {item.Value} шт"); }
            }
            baseChangedEvent?.Invoke(null, guest);

            return sushiList;
        }
    }
}
