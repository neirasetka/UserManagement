using System;
using System.Threading.Tasks;

namespace UserManagement.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmail(string to, string from, string subject, string message, DateTime date);
    }
}