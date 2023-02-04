using LCS.Domain.Models;
using LCS.WebApi.Response.Base;

namespace LCS.WebApi.Response
{
    public class LawyerResponse: PersonResponse
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
        public List<LanguageResponse> Languages { get; set; }
        public List<TimeSlotResponse> WorkingSlots { get; set; }
        public string? OfficeAddress { get; set; }

        public static implicit operator LawyerResponse(Lawyer model)
        {

            var langs = model.Languages.Select(x => new LanguageResponse
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate
            }).ToList();
            var depts = model.Departments.Select(x => new DepartmentResponse
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                Name = x.Name,
            }).ToList();

            var slots = model.WorkingSlots.Select(x => new TimeSlotResponse
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                Index = x.Index,
                StartHour = x.StartHour,
                StartMinute = x.StartMinute,
                EndHour = x.EndHour,
                EndMinute = x.EndMinute,
            }).ToList();
            return new LawyerResponse()
            {
                Id = model.Id,
                Email = model.Email,
                CreatedDate = model.CreatedDate,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                DOB = model.DOB,
                PhoneNo = model.PhoneNo,
                Gender = model.Gender.ToString(),
                AcceptOfflineAppointment = model.AcceptOfflineAppointment,
                AcceptOnlineAppointment = model.AcceptOnlineAppointment,
                OnlineCharge = model.OnlineCharge,
                OfflineCharge = model.OfflineCharge,
                OfficeAddress = model.OfficeAddress,
                IsVerify = model.IsVerify,
                Title = model.Title,
                OfficeEmail = model.OfficeEmail,
                Languages = langs,
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
