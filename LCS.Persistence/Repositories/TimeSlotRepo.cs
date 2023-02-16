using Law.Domain.Models;
using Law.Domain.Repositories;
using Law.Persistence.Data;

namespace Law.Persistence.Repositories
{
    public class TimeSlotRepo : BaseRepo<TimeSlot>, ITimeSlotRepo
    {
        public TimeSlotRepo(LCSDbContext context) : base(context)
        {
        }

    }
}
