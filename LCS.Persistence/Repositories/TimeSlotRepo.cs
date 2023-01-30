using LCS.Domain.Entities;
using LCS.Domain.Models;
using LCS.Domain.Repositories;
using LCS.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Repositories
{
    public class TimeSlotRepo : BaseRepo<TimeSlotTB>, ITimeSlotRepo
    {
        public TimeSlotRepo(LCSDbContext context) : base(context)
        {
        }

        public List<TimeSlot> Convertlist(List<TimeSlotTB> listTB)
        {
            var res=new List<TimeSlot>();
            foreach (var t in listTB)
            {
                res.Add(t);
            }
            return res;
        }
    }
}
