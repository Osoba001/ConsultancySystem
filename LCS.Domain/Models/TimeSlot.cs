using LCS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class TimeSlot:ModelBase
    {
        
        public int Index { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
       

        public override string ToString()
        {
          return  $"{StartHour:00}:{StartMinute:00} - {EndHour:00}:{EndMinute:00}";
        }
    }
}
