using OrderManagmentSystem.Core.Services.EmailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Services
{
    public class SmtpEmailService:IEmailServices
	{
		private readonly SmtpClient _smtpClient;

		public SmtpEmailService(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
		{
			_smtpClient = new SmtpClient
			{
				Host = smtpHost,
				Port = smtpPort,
				UseDefaultCredentials = false,
				Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword),
				EnableSsl = true
			};
		}
		          

		public async Task SendEmailAsync(MailMessage message)
		{
			await _smtpClient.SendMailAsync(message);
		}
	}
}
