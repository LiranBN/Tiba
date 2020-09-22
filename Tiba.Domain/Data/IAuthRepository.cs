using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiba.Domain.Models;

namespace Tiba.Domain.Data
{
    public interface IAuthRepository
    {
        Task<User> LoginAsync(string userName, string password);

        Task<User> RegisterAsync(User user, string password);

        Task<bool> UserExsitsAsync(string userName);
    }
}
