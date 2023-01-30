using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class TimeSlotTB:EntityBase
    {
        public TimeSlotTB()
        {
            WorkingSlots = new();
        }
        public int Index { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public List<WorkingSlotTB> WorkingSlots { get; set; }

        public static implicit operator TimeSlot(TimeSlotTB timeSlotTB)
        {
            return new TimeSlot()
            {
                Id = timeSlotTB.Id,
                Index = timeSlotTB.Index,
                StartHour = timeSlotTB.StartHour,
                StartMinute = timeSlotTB.StartMinute,
                EndHour = timeSlotTB.EndHour,
                EndMinute = timeSlotTB.EndMinute,
                CreatedDate= timeSlotTB.CreatedDate
            };
        }
       
    }
}
