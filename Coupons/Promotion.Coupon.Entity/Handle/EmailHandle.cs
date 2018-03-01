using System.Net;
using System.Net.Mail;

namespace Promotion.Coupon.Entity.Handle
{
    public class EmailHandle
    {
        public static void SendEmail(string from, string to, string subject, string message, string ambiente)
        {

            SmtpClient mySmtpClient = new SmtpClient("mailhost.gwsweb.net");
            mySmtpClient.Port = 25;
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.NetworkCredential basicAuthenticationInfo = new
            System.Net.NetworkCredential("noreply@relay.kccweb.net", "Pass475866");
            mySmtpClient.Credentials = basicAuthenticationInfo;
                        
            //INTERNO
            SmtpClient mySmtpClient2 = new SmtpClient("smtp.gmail.com");
            mySmtpClient2.Port = 587;
            mySmtpClient2.EnableSsl = true;
            mySmtpClient2.UseDefaultCredentials = false;
            mySmtpClient2.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.NetworkCredential basicAuthenticationInfo2 = new
            System.Net.NetworkCredential("gomes.vmlbrasil@gmail.com", "z3277009@");                
            mySmtpClient2.Credentials = basicAuthenticationInfo2;
                       
           
            MailAddress fromT = new MailAddress(from, "Promoção Intimus Sport");
            MailAddress toT = new MailAddress(to, "");
            MailMessage myMail = new System.Net.Mail.MailMessage(fromT, toT);
       
            // set subject and encoding
            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = message;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;
            if (ambiente == "Interno")
            {
                mySmtpClient2.Send(myMail);
            }
            else
            {
                mySmtpClient.Send(myMail);
            }
        }
    }
}
