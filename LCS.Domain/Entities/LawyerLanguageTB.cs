using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class LawyerLanguageTB:EntityBase
    {
        public LanguageTB Language { get; set; }
        public LawyerTB Lawyer { get; set; }
    }
}
