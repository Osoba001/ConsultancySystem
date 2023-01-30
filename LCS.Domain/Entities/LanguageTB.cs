using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Entities
{
    public class LanguageTB:EntityBase
    {
        public string Name { get; set; }

        public static implicit operator Language(LanguageTB tb)
        {
            return new Language() { Id = tb.Id, Name = tb.Name };
        }
    }
}
