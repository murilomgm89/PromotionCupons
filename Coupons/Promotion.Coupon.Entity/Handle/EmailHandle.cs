using System.Net;
using System.Net.Mail;

namespace Promotion.Coupon.Entity.Handle
{
    public class EmailHandle
    {
        public static void SendEmail(string from, string to, string subject, string message)
        {
            //Exemplo de chamada
            //var credential = new NetworkCredential("naoresponda@oi.com.br", "oioi2015");
            //EmailHandle.SendEmail(from, to, "Comparação de planos", bd, credential);
            
            using (var mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("gympass@gmail.com.br", "gympass");
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}
