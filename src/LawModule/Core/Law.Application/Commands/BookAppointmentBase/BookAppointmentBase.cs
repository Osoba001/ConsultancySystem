using Law.Domain.Models;
using ShareServices.Events;
using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ActionResponse;

namespace Law.Application.Commands.AppointmentB
{
    public class BookAppointmentBase:ILawAppointmentEvent
    {
        public event EventHandler<BookedAppointmentEventArg>? BookedAppointmentEvent;

        protected virtual void OnBookedAppointment(BookedAppointmentEventArg arg)
        {
            BookedAppointmentEvent?.Invoke(this, arg);
        }
        public void BookAppointmentEventManager(Appointment appointment)
        {

            var bkAppAgr = new BookedAppointmentEventArg
            {
                AppointmentName = appointment.AppointmentType.ToString(),
                Receiver = appointment.Lawyer.FirstName,
                Client = appointment.Client.FirstName,
                ReceiverEmail = appointment.Lawyer.Email,
                ClientEmail = appointment.Client.Email,
                ReviewAddress = $"{appointment.Lawyer.OfficeAddress},{appointment.Lawyer.Location}, {appointment.Lawyer.State}",
                ReviewDate = appointment.ReviewDate,
            };
            OnBookedAppointment(bkAppAgr);
        }
    }
}