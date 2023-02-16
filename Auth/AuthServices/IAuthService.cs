using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.DTO;
using User.Application.Entities;

namespace User.Application.AuthServices
{
    public interface IAuthService
    {
        Task<TokenModel> TokenManager(UserTb user);

        void PasswordManager(string password, UserTb user);

        bool VerifyPassword(string password, UserTb user);
    }
}
