using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.Models
{
    public class CreatedUserDTO
    {
        public Guid Id { get; }
        public string Email { get; }
        public string FirstName { get; }
    }
}
