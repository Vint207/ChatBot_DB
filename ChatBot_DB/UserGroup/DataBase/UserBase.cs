using System;
using System.Collections.Generic;

namespace Chat_Bot
{
    public class UserBase : ICRUD<UserMiddle>
    {

        List<UserMiddle> _itemList = new() { new() { Name = "Admin", Password = "admin01", Mail = "admin@mail.com" } };
        public event BaseChangedEvent<UserMiddle> baseChangedEvent;

        public bool AddItem(UserMiddle user)
        {
            if (user != null)
            {
                _itemList.Add(user);
                baseChangedEvent?.Invoke(user);
                return true;
            }
            return false;
        }

        public bool DeleteItem(UserMiddle user)
        {
            user = _itemList.Find(item => item.Mail == user.Mail && item.Password == user.Password);

            if (user != null)
            {
                _itemList.Remove(user);
                baseChangedEvent?.Invoke(user);
                return true;
            }
            return false;
        }

        public UserMiddle GetItem(UserMiddle user)
        {
            user = _itemList.Find(item => item.Mail == user.Mail && item.Password == user.Password);

            if (user != null)
            {
                baseChangedEvent?.Invoke(user);
                return user;
            }
            return null;
        }

        public bool GetAllItemsInfo(UserMiddle user)
        {
            foreach (var item in _itemList) 
            { item.GetInfo(); }

            baseChangedEvent?.Invoke(user);

            return true;
        }
    }
}
