using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace ProjectTemplate.Business
{
    class EmailProvider
    {
        /// <summary>
        /// Sendet anhand Sender und Empfänger E-Mail Adresse und Daten ein E-Mail
        /// </summary>
        /// <param name="fromemailaddr"></param>
        /// <param name="toemailaddr"></param>
        /// <param name="password"></param>
        /// <param name="data"></param>
        public void send(string fromemailaddr, string toemailaddr, string password, string data)
        {
            try
            {
                var fromAddress = new MailAddress(fromemailaddr);
                var toAddress = new MailAddress(toemailaddr);
                string fromPassword = password;
                string subject = "Subject";
                string body = data;

                var smtp = new SmtpClient
                {
                    Host = "smtp.live.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromemailaddr, toemailaddr)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch
            {

            }
        }
    }
}
