using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Entities
{
    public class AssignedUserRoleTb:EntityBase
    {
        public AssignedUserRoleTb()
        {
                
        }
        public AssignedUserRoleTb(UserTb user, UserRoleTb role)
        {
            Role=role;
            User =user;
        }
        public UserRoleTb Role { get; set; }
        public UserTb User { get; set; }
    }
}
