using Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.UserServices
{
    public interface IUserRoleService
    {
        Task<ActionResult> AddRole(string name);
        Task<ActionResult> RemoveRole(string name);
        Task<List<string>> GetAllRoles();
    }
}
