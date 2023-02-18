using User.Application.Entities;

namespace User.Application.DTO
{
    public class UserResponse
    {
        public UserResponse()
        {

        }
        public UserResponse(Guid id, string firstName, string email, string gender, DateTime dob)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            Gender = gender;
            DOB = dob;
        }
        
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string? Location { get; set; }
        public string? State { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Age => ComputeAge();
        private int ComputeAge()
        {
            int age = DateTime.Now.Year - DOB.Year;
            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                age--;
            return age;
        }
        public static implicit operator UserResponse(UserTb user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                MiddleName = user.FirstName,
                LastName = user.LastName,
                PhoneNo = user.PhoneNo,
                DOB = user.DOB,
                Gender = user.Gender.ToString(),
                Location = user.Location,
                State = user.State,
                CreatedDate=user.CreatedDate
            };
        }
    }

}
