using LCS.Domain.Entities;
using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface ITimeSlotRepo:IBaseRepo<TimeSlotTB>
    {
        List<TimeSlot> Convertlist(List<TimeSlotTB> listTB);
    }
}
