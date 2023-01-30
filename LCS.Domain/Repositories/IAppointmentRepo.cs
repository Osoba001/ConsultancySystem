using LCS.Domain.Constants;
using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface IAppointmentRepo: IBaseRepo<AppointmentTB>
    {
        List<Appointment> Convertlist(List<AppointmentTB> listTB);
    }
}
