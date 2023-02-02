using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Entities
{
    public class UserTb: EntityBase
    {
        
         public UserTb(string name, string email)
        {
            Name = name;
            Email = email;
            AssignedUserRoles = new();
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<AssignedUserRoleTb> AssignedUserRoles { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? RefreshTokenExpireTime { get; set; }
        public int RecoveryPin { get; set; }
        public DateTime? RecoveryPinExpireTime { get; set; }
        public static implicit operator User(UserTb user)
        {
            return new User(user.Id, user.Name, user.Email);
        }
        

    }
}
