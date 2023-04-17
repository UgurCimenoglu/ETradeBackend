using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Abstracts.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] to, string subject, string body, bool isBodyHtml = true);
        Task SendResetPasswordMailAsync(string to,string userId,string resetToken);
    }
}
