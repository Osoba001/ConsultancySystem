using Law.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using ShareServices.EmailService;
using ShareServices.Models;

namespace Law.Application.Commands.AppointmentB
{
    public class BookAppointmentBase
    {
       
        public void BookedAppointmentEmailSender(Appointment appointment, IServiceProvider ServiceProvider)
        {

            var bkApp = new BookedAppointmentDTO
            {
                AppointmentName = appointment.AppointmentType.ToString(),
                Receiver = appointment.Lawyer.FirstName,
                Client = appointment.Client.FirstName,
                ReceiverEmail = appointment.Lawyer.Email,
                ClientEmail = appointment.Client.Email,
                ReviewAddress = $"{appointment.Lawyer.OfficeAddress},{appointment.Lawyer.Location}, {appointment.Lawyer.State}",
                ReviewDate = appointment.ReviewDate,
            };
            var emailSender = ServiceProvider.GetRequiredService<IEmailSender>();
            emailSender.SendOnBookedAppointmentEmailAsync(bkApp);
        }
    }
}