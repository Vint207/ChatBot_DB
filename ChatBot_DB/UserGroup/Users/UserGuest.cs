namespace ChatBot_DB
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
