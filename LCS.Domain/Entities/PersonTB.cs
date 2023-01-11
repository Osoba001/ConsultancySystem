using LCS.Domain.Constants;

namespace LCS.Domain.Entities
{
    public class PersonTB:EntityBase
    {
        public PersonTB(UserTB user)
        {
            User = user;
        }
        public UserTB User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime DOB { get; set; }

        
    }
}
