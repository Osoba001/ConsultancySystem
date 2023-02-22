using ShareServices.RedisMsgDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.EmailService
{
    public interface IEmailSender
    {
        void SendRecoverPinEmailAsync(string RecieverEmail, int pin);
        void SendOnBookedAppointmentEmailAsync(BookedAppointmentDTO bookedAppointmentDTO);
        ValueTask<bool> SendEmailAsync(string RecieverEmail, string subject, string body);
        ValueTask<bool> SendEmailAsync(List<string> RecieverEmail, string subject, string body);
    }
}
