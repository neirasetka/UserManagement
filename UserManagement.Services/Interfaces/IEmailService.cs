using System;
using System.Threading.Tasks;

namespace UserManagement.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmail(string to, string from, string message, DateTime date);
    }
}