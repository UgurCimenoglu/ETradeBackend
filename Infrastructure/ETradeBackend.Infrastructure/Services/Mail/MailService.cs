using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Abstracts.Services;
using Microsoft.Extensions.Configuration;

namespace ETradeBackend.Infrastructure.Services.Mail
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {

            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            mail.Body = body;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.From = new MailAddress(_configuration["Mail:Username"], "UGUR-ETICARET", Encoding.UTF8);
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.EnableSsl = true;
            smtp.Port = Int32.Parse(_configuration["Mail:Port"]);
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendResetPasswordMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine(
                "Merhaba <br> Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz...<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tklayınız...</a></strong><br><br><span style:\"font-size:12px;\"> Not: Eğer bu talep tarafınızca gerçekleşmedi ise lütfen bu maili ciddiye almayınız</span><br>");

            await SendMailAsync(to, "Şifre Yenileme Talebi - UgurETicaret", mail.ToString());
        }
    }
}
