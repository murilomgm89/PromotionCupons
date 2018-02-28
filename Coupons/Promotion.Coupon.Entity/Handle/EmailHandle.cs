using System.Net;
using System.Net.Mail;

namespace Promotion.Coupon.Entity.Handle
{
    public class EmailHandle
    {
        public static void SendEmail(string from, string to, string subject, string message)
        {
            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
            mySmtpClient.Port = 587;
            mySmtpClient.EnableSsl = true;
            mySmtpClient.UseDefaultCredentials = true;
            
            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
            System.Net.NetworkCredential("gomes.vmlbrasil@gmail.com", "z3277009@");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress fromT = new MailAddress(from, "Promoção Intimus Sport");
            MailAddress toT = new MailAddress(to, "");
            MailMessage myMail = new System.Net.Mail.MailMessage(fromT, toT);

            //// add ReplyTo
            //MailAddress replyto = new MailAddress("reply@example.com");
            //myMail.ReplyToList.Add(replyTo);

            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = message;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
        }
    }
}
