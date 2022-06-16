using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string to, string from, string message, DateTime date);
    }
}
