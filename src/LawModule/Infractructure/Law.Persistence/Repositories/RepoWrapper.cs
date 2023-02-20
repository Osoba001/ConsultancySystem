using Law.Domain.Repositories;
using Law.Persistence.Data;
using Utilities.ActionResponse;

namespace Law.Persistence.Repositories
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly LawDbContext _context;

        public RepoWrapper(LawDbContext context)
        {
            _context = context;
        }

        private IAppointmentRepo? appointmentRepo;
        public IAppointmentRepo AppointmentRepo
        {
            get
            {
                appointmentRepo ??= new AppointmentRepo(_context);
                return appointmentRepo;
            }
        }

        private IClientRepo? clientRepo;
        public IClientRepo ClientRepo
        {
            get
            {
                clientRepo ??= new ClientRepo(_context);
                return clientRepo;
            }
        }

        private IDepartmentRepo? departmentRepo;
        public IDepartmentRepo DepartmentRepo
        {
            get
            {
                departmentRepo ??= new DepartmentRepo(_context);
                return departmentRepo;
            }
        }

        private ILawyerRepo? lawyerRepo;
        public ILawyerRepo LawyerRepo
        {
            get
            {
                lawyerRepo ??= new LawyerRepo(_context);
                return lawyerRepo;
            }
        }
        private ITimeSlotRepo? timeSlotRepo;
        public ITimeSlotRepo TimeSlotRepo
        {
            get
            {
                timeSlotRepo ??= new TimeSlotRepo(_context);
                return timeSlotRepo;
            }
        }
        public ActionResult FailedAction(string message)
        {
            var res = new ActionResult();
            res.AddError(message);
            return res;
        }

        public ActionResult<U> FailedAction<U>(string message) where U : class
        {
            var res = new ActionResult<U>();
            res.AddError(message);
            return res;
        }
    }
}
