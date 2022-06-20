﻿using Hangfire;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class EmailService : IEmailService
    {
        public async Task<string> SendEmail(string receiver, string receiverName, string subject, string Message, DateTime date)
        {
            if (date == default(DateTime))
                return BackgroundJob.Schedule(() => Send(receiver, receiverName, subject, Message), DateTime.Now.AddSeconds(10));
            else
            {
                var dateTimeOffset = date.AddDays(-10);
                return BackgroundJob.Schedule(() => Send(receiver, receiverName, subject, Message), dateTimeOffset);
            }
        }

        public async Task<string> Send(string receiver, string receiverName, string Subject, string message)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mirza.lepir13@gmail.com", "UserManagement");
            var subject = Subject;
            var to = new EmailAddress(receiver, receiverName);
            var plainTextContent = $"{message}";
            var htmlContent = $"<strong>{plainTextContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.ToString();
        }
    }
}