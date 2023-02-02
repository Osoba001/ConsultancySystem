using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Entities
{
    public class UserRoleTb: EntityBase
    {
        public UserRoleTb(string name)
        {
            Name = name;
            AssignedUserRoles = new();
        }
        public string Name { get; set; }
        public List<AssignedUserRoleTb> AssignedUserRoles { get; set; }
    }
}
