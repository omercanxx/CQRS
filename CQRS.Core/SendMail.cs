using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public class SendMail : ISendMail
    {
        private readonly IConfiguration _configuration;
        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(string email, string subject, string body)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            sc.Credentials = new NetworkCredential(_configuration["Mail:Sender"], _configuration["Mail:Password"]);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_configuration["Mail:Sender"], _configuration["Mail:Title"]);

            mail.To.Add(email);

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            sc.Send(mail);
        }
    }
}
