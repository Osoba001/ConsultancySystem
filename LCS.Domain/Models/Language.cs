using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Models
{
    public class Language:ModelBase
    {
        public Language(string name)
        {
            Name = name;
        }
        public Language()
        {

        }
        public string Name { get; set; }
    }
}
