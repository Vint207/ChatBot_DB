namespace Chat_Bot
{
    public class UserGuest : User
    {

        public UserGuest() 
        {
            Name = "Гость";
            ID = new(); 
        }
    }
}
