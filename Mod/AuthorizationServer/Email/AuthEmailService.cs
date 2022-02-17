using AuthorizationServer.Email.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Email
{
    public class AuthEmailService
    {
        private readonly EmailSettingsModel _settings;
        private readonly string _serverName;
        public AuthEmailService(IConfiguration configuration)
        {
            _settings = new EmailSettingsModel();
            configuration.GetSection("EmailSettings").Bind(_settings);
            _serverName = configuration.GetValue<string>("ServerName");
        }

        public async Task<string> SendEmailWithConfirmationCode(string to)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_serverName, _settings.Account));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = "Email confirmation code";
            var code = GenerateConfirmCode();
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = code
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_settings.SMTP, _settings.Port, _settings.SSL);
                await client.AuthenticateAsync(_settings.Account, _settings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            return code;
        }

        private string GenerateConfirmCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
