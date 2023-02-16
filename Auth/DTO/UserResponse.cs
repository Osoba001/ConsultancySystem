namespace User.Application.DTO
{
    public class UserResponse
    {
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
        public int Age => ComputeAge();
        private int ComputeAge()
        {
            int age = DateTime.Now.Year - DOB.Year;
            if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                age--;
            return age;
        }
    }

}
