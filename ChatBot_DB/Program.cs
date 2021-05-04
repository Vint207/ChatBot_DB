using System.Threading.Tasks;

namespace Chat_Bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            UsersDB usersMSSql = new();

            await usersMSSql.CreateItem(new() {Name = "Додик", Password = "123456", Mail = "dodik@mail.tg" });

            

            ////UserBase users = new();
            //SushiBase sushi = new();

            //new ChatBot(users, sushi).MainMenu();

            //if (connection.State == ConnectionState.Open)
            //{
            //    await connection.CloseAsync();
            //    Console.WriteLine("Подключение закрыто...");
            //}
        }
    }
}
