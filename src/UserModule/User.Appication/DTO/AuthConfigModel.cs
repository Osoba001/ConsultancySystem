using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO
{
    public class AuthConfigModel
    {
        public string SecretKey { get; set; }
        public int AccessTokenLifespanInMins { get; set; }
        public int RefreshTokenLifespanInMins { get; set; }

    }
}
