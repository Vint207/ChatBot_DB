using System;
using System.Collections.Generic;

namespace ChatBot_DB
{
    public class ProtoUserBase : ICRUD<User>
    {

        List<User> _itemList = new() { new() { Name = "Admin", Password = "admin01", Mail = "admin@mail.com" } };
        public event BaseChangedEvent<User> baseChangedEvent;

        public bool AddItem(User ProtoUser)
        {
            if (ProtoUser != null)
            {
                _itemList.Add(ProtoUser);
                baseChangedEvent?.Invoke(ProtoUser);
                return true;
            }
            return false;
        }

        public bool DeleteItem(User ProtoUser)
        {
            ProtoUser = _itemList.Find(item => item.Mail == ProtoUser.Mail && item.Password == ProtoUser.Password);

            if (ProtoUser != null)
            {
                _itemList.Remove(ProtoUser);
                baseChangedEvent?.Invoke(ProtoUser);
                return true;
            }
            return false;
        }

        public User GetItem(User ProtoUser)
        {
            ProtoUser = _itemList.Find(item => item.Mail == ProtoUser.Mail && item.Password == ProtoUser.Password);

            if (ProtoUser != null)
            {
                baseChangedEvent?.Invoke(ProtoUser);
                return ProtoUser;
            }
            return null;
        }

        public bool GetAllItemsInfo(User ProtoUser)
        {
            foreach (var item in _itemList) 
            { item.GetInfo(); }

            baseChangedEvent?.Invoke(ProtoUser);

            return true;
        }
    }
}
