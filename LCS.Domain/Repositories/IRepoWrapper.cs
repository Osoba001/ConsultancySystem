using LCS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface IRepoWrapper
    {
        IAppointmentRepo AppointmentRepo { get; }
        IClientRepo ClientRepo { get; }
        IDepartmentRepo DepartmentRepo { get; }
        ILanguageRepo LanguageRepo { get; }
        ILawyerRepo LawyerRepo { get; }
        ITimeSlotRepo TimeSlotRepo { get; }
        ActionResult FailedAction(string message);
        ActionResult<U> FailedAction<U>(string message) where U : class;
    }
}
