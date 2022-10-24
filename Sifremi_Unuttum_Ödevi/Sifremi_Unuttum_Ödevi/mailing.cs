using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Sifremi_Unuttum_Ödevi
{
    internal class mailing
    {
        public void sendmail()
        {
            var client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("sifremi.unuttum.info@gmail.com","itxgawdphpcjebhj"),
                EnableSsl = true,
                
            };
            var mailmessage = new MailMessage("sifremi.unuttum.info@gmail.com","jkgnjb@gmail.com","konu","mesaj");

            client.Send(mailmessage);
            
        }

    }
}
