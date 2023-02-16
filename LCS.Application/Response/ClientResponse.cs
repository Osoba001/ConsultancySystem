using Law.Application.Response.Base;
using Law.Domain.Models;

namespace Law.Application.Response
{
    public class ClientResponse : PersonResponse
    {
        public ClientResponse()
        {
            Appointments = new();
        }
        public List<AppointmentResponse> Appointments { get; set; }
        public static implicit operator ClientResponse(Client model)
        {
            return new ClientResponse()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                Email = model.Email,
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
