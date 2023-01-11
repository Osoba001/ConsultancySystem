using LCS.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class UserTB:EntityBase
    {
        public UserTB(string name, string email, Role role, byte[] pswdHash, byte[] pswdSalt)
        {
            Name = name;
            Email = email;
            Role = role;
            HashPassword = pswdHash;
            SaltPassword = pswdSalt;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }
    }
}
