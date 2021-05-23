using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Helpers
{
    public class EmailService
    {
        IConfiguration configuration;
        public string username { get; set; }
        public string pwd { get; set;}
            public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private MailMessage ConfigureMail(string asunto, string from, string message)
        {
            MailMessage mail = new MailMessage();
            string username = this.configuration["usuariomail"];
            string pwd = this.configuration["passwordmail"];
            //pongo simepre que lo reciba essup.net.core@gmail.com 
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(username));
            mail.Subject = asunto;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            string smtpserver = this.configuration["host"];
            int port =int.Parse(this.configuration["port"]);
            bool ssl = bool.Parse(this.configuration["ssl"]);
            bool defaultcredentials = bool.Parse(this.configuration["defaultcredentials"]);
            return mail;
        }

        private void ConfigureSmtp(MailMessage mail)
        {
                
            bool defaultcredentials = bool.Parse(this.configuration["defaultcredentials"]);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host= this.configuration["host"];
            smtpClient.Port=int.Parse(this.configuration["port"]);
            smtpClient.EnableSsl=bool.Parse(this.configuration["ssl"]);
            smtpClient.Credentials = new NetworkCredential(this.configuration["usuariomail"], 
                this.configuration["passwordmail"]);
            smtpClient.Send(mail);
        }
        public int SendMail(String to, String asunto, String message)
        {
            try
            {
            MailMessage mail = this.ConfigureMail(asunto, to, message);
            this.ConfigureSmtp(mail);
                return 1;
            }catch(Exception e)
            {
                return 0;
            }
           
        }

    }
}
