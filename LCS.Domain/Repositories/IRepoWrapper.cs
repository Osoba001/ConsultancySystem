using Utilities.ActionResponse;

namespace Law.Domain.Repositories
{
    public interface IRepoWrapper
    {
        IAppointmentRepo AppointmentRepo { get; }
        IClientRepo ClientRepo { get; }
        IDepartmentRepo DepartmentRepo { get; }
        ILawyerRepo LawyerRepo { get; }
        ITimeSlotRepo TimeSlotRepo { get; }
        ActionResult FailedAction(string message);
        ActionResult<U> FailedAction<U>(string message) where U : class;
    }
}
