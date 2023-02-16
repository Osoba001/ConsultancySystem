namespace Law.Domain.Models
{
    public class Lawyer : Person
    {
        public Lawyer()
        {
            Departments = new();
            Appointments = new();
            Languages = new() { "English" };
            OnlineWorkingSlots = new();
        }

        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public bool IsVerify { get; set; }
        public string? OfficeEmail { get; set; }
        public string Title { get; set; }
        public string PhoneNo { get; set; }
        public List<Department> Departments { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<string> Languages { get; set; }
        public List<TimeSlot> OnlineWorkingSlots { get; set; }
        public List<TimeSlot> OfflineWorkingSlots { get; set; }
        public string? OfficeAddress { get; set; }
        public string? Location { get; set; }
        public string? State { get; set; }
    }
}
