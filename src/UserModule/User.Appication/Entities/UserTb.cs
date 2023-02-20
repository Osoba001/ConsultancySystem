using ShareServices.Constant;
using User.Application.Constants;

namespace User.Application.Entities
{
    public class UserTb : EntityBase
    {

        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string? Location { get; set; }
        public string? State { get; set; }
        public string Email { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? RefreshTokenExpireTime { get; set; }
        public int RecoveryPin { get; set; }
        public DateTime? RecoveryPinExpireTime { get; set; }
        public byte[]? ImageArray { get; set; }
    }
}
