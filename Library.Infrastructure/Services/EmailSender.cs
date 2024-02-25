﻿using Library.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Library.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly Sender _sender;
        public EmailSender(IOptions<Sender> sender)
        {
            _sender = sender.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromMail = _sender.FromMail;
            var fromPassword = _sender.FromPassword;

            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_sender.Client)
            {
                Port = _sender.Port,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }

}
