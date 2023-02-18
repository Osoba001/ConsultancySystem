using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.RedisMsgDTO
{
    public class RecoveringPasswordDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int Pin { get; set; }
    }
}
