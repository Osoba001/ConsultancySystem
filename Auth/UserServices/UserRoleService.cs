using Auth.Entities;
using Auth.Repository;
using Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.UserServices
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepo _roleRepo;

        public UserRoleService(IUserRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public async Task<ActionResult> AddRole(string name)
        {
            name=name.Trim();
            var item= await _roleRepo.FindOneByPredicate(x=>x.Name.ToLower()==name.ToLower());
            if (item == null)
                return await _roleRepo.Add(new UserRoleTb(name));
            var res = new ActionResult();
            res.AddError("This role already exist.");
            return res;
        }

        public async Task<List<string>> GetAllRoles()
        {
            var res = await _roleRepo.GetAll();
            var rols= from r in res select r.Name;
            return rols.ToList();
        }

        public async Task<ActionResult> RemoveRole(string name)
        {
            name = name.Trim();
            var item = await _roleRepo.FindOneByPredicate(x => x.Name.ToLower() == name.ToLower());
            if (item != null)
                return await _roleRepo.Delete(item);
            var res = new ActionResult();
            res.AddError("This role does not exist.");
            return res;
        }
    }
}
