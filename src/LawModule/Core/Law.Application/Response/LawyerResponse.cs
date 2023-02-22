using Law.Application.Response.Base;
using Law.Domain.Models;

namespace Law.Application.Response
{
    public class LawyerResponse : PersonResponse
    {
        public bool AcceptOnlineAppointment { get; set; }
        public bool AcceptOfflineAppointment { get; set; }
        public double OnlineCharge { get; set; }
        public double OfflineCharge { get; set; }
        public bool IsVerify { get; set; }
        public string? OfficeEmail { get; set; }
        public string Title { get; set; }
        public List<DepartmentResponse> Departments { get; set; }
        public List<AppointmentResponse> Appointments { get; set; }
        public List<string> Languages { get; set; }
        public List<TimeSlotResponse> WorkingSlots { get; set; }
        public string? OfficeAddress { get; set; }

        public static implicit operator LawyerResponse(Lawyer model)
        {
            var depts = model.Departments.Select(x => new DepartmentResponse
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                Name = x.Name,
            }).ToList();

            var slots = model.OnlineWorkingSlots.Select(x => new TimeSlotResponse
            {
                Id = x.Id,
                Index = x.Index,
                StringValue = x.ToString(),
            }).ToList();
            return new LawyerResponse()
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                OnlineCharge = model.OnlineCharge,
                OfflineCharge = model.OfflineCharge,
                IsVerify = model.IsVerify,
                Title = model.Title,
                OfficeEmail = model.OfficeEmail,
                Languages = model.Languages,
                Departments = depts,
                WorkingSlots = slots,
                Appointments = ConverLangs(model.Appointments)
            };
        }

        private static List<AppointmentResponse> ConverLangs(List<Appointment> aptmtsTb)
        {
            List<AppointmentResponse> aptmts = new();
            foreach (var aptmt in aptmtsTb)
            {
                aptmts.Add(aptmt);
            }
            return aptmts;
        }
    }
}
