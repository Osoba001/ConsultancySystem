namespace LCS.WebApi.Response.Base
{
    public class PersonResponse:BaseResponse
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string? Gender { get; set; }
        public string? PhoneNo { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
