using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Services.EmailService
{
    public interface IEmailServices
    {
        Task SendEmailAsync(MailMessage message);
     
    }
}
