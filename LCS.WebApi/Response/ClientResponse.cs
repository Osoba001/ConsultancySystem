using LCS.Domain.Models;
using LCS.WebApi.Response.Base;

namespace LCS.WebApi.Response
{
    public class ClientResponse: PersonResponse
    {
        public List<AppointmentResponse> Appointments { get; set; }
        public static implicit operator ClientResponse(Client model)
        {
            return new ClientResponse()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                PhoneNo = model.PhoneNo,
                Gender = model.Gender.ToString(),
                DOB = model.DOB,
                CreatedDate = model.CreatedDate,
                Appointments = ConvertAptmt(model.Appointments)
            };
        }
        private static List<AppointmentResponse> ConvertAptmt(List<Appointment> listtb)
        {
            var aptmts = new List<AppointmentResponse>();
            foreach (Appointment aptmt in listtb)
            {
                aptmts.Add(aptmt);
            }
            return aptmts;
        }
    }
}
