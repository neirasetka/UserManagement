using Hangfire;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmail(string receiver, string receiverName, string expenseName, DateTime date)
        {
            var dateTimeOffset = date.AddDays(-10);
            var job = BackgroundJob.Schedule(() => Send(receiver, receiverName, expenseName, date), dateTimeOffset);
            if (job == null)
                return false;
            return true;
        }

        public async Task<bool> Send(string receiver, string receiverName, string expenseName, DateTime date)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mirza.lepir13@gmail.com", "RegistrationCompany");
            var subject = "Your Registration Is Running Out!";
            var to = new EmailAddress(receiver, receiverName);
            var plainTextContent = $"Your {expenseName} is running out in 10 days!";
            var htmlContent = $"<strong>{plainTextContent}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);
            return true;
        }
    }
}