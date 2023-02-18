using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.Events
{
    public interface IAppointmentEvent
    {
        event EventHandler<BookedAppointmentEventArg>? BookedAppointmentEvent;
    }
}
