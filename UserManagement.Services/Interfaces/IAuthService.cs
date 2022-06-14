using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(User newUser);
    }
}

