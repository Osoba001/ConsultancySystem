using Auth.Entities;
using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthServices
{
    public interface IAuthService
    {
        Task<TokenModel> TokenManager(UserTb user);

        void PasswordManager(string password, UserTb user);

        bool VerifyPassword(string password, UserTb user);
    }
}
