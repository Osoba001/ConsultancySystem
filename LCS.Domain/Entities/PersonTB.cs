using LCS.Domain.Constants;

namespace LCS.Domain.Entities
{
    public class PersonTB:EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public Gender? Gender { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }

  
    }
}
