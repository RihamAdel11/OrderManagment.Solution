using Microsoft.AspNetCore.Identity;
using OrderManagmentSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentSystem.Core.Services
{
    public interface IAuthServices
    {
        Task<string> CreateTokenAsync(User User, UserManager<User> userManager);
    }
}
