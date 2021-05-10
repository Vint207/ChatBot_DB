using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChatBot_DB
{

    public delegate void EventEmail(string head, string body);

    class EmailService
    {

        public static void SendEmail(string head, string body)
        {
            //MailAddress from = new MailAddress("zhlsgt@gmail.com", "Приложение");
            //MailAddress to = new MailAddress("zhlsgt@gmail.com");
            //MailMessage m = new MailMessage(from, to); 
            //m.Subject = head;
            //m.Body = $"<h2>{body}</h2>";
            //m.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Credentials = new NetworkCredential("zhlsgt@gmail.com", "Zhlsgt@gmail.com_97");
            //smtp.EnableSsl = true;
            //smtp.Send(m);
        }
    }
}
