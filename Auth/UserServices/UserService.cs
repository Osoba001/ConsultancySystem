using Auth.AuthServices;
using Auth.Entities;
using Auth.Models;
using Auth.Repository;
using Auth.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserRoleRepo _roleRepo;
        private readonly IAssignedUserRoleRepo _assignedUserRoleRepo;
        private readonly IAuthService _authService;

        public UserService(IUserRepo userRepo, IUserRoleRepo roleRepo, IAssignedUserRoleRepo assignedUserRoleRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _assignedUserRoleRepo = assignedUserRoleRepo;
            _authService = authService;
        }
        public async Task<ActionResult> AddRoleToUser(Guid id, string role)
        {
            role = role.Trim().ToLower();
            var res = new ActionResult();
            var user = await _userRepo.FindById(id);
            if (user!=null)
            {
                var userRole = await _roleRepo.FindOneByPredicate(x => x.Name.ToLower() == role);
                {
                    if (userRole != null)
                        return await _assignedUserRoleRepo.Add(new AssignedUserRoleTb(user, userRole));
                    else
                        res.AddError($"Role ({role})  does not exist.");
                }
            }
            else
                res.AddError($"User  does not exist.");
            return res;
        }

        public async Task<ActionResult> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var res = new ActionResult();
            var user = await _userRepo.FindById(id);
            if (user != null)
            {
                if (_authService.VerifyPassword(oldPassword, user))
                {
                    _authService.PasswordManager(newPassword, user);
                    return await _userRepo.Update(user);
                }
                else
                    res.AddError("Incorrect password.");
            }
            else
                res.AddError($"Email is invalid.");
            return res;
        }
        public async Task<ActionResult> FalseDeleteUser(Guid id)
        {
            var user = await _userRepo.FindById(id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedDate = DateTime.UtcNow;
                return await _userRepo.Update(user);
            }
            return FailActionResult("User not found.");
        }
        public async Task<ActionResult> UndoFalseDelete(Guid id)
        {
            var user = await _userRepo.FindById(id);
            if (user != null)
            {
                if (!user.IsDeleted)
                    return new ActionResult();
                user.IsDeleted = false;
                return await _userRepo.Update(user);
            }
            return FailActionResult("User not found.");
        }
        public async Task<ActionResult> HardDeleteUser(Guid id)
        {
            var user = await _userRepo.FindById(id);
            if (user != null)
                return await _userRepo.Delete(user);
           return FailActionResult("User not found.");
        }

        public async Task<int> ForgottenPassword(string email)
        {
            email = email.Trim().ToLower();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email);
            if (user != null)
            {
                user.RecoveryPin = RandomPin();
                await _userRepo.Update(user);
                return user.RecoveryPin;
            }
            else
                return -1;
        }

        public async Task<List<User>> AllUsers()
        {
             return ConvertUser(await _userRepo.GetAll());
        }
        public async Task<List<User>> GetFalseDeletedUsers(int days = 0)
        {
            return ConvertUser(await _userRepo
                .IgnorQueryFilter(x=>x.IsDeleted && x.DeletedDate !=null && DateTime.UtcNow.Day- x.DeletedDate.Value.Day>=days));
        }

        public async Task<List<User>> UsersByRoles(string role)
        {
            role = role.Trim().ToLower();
            var assigned=await _assignedUserRoleRepo.FindByPredicate(x=>x.Role.Name.ToLower()==role);
            var users = from assig in assigned select assig.User;
            return ConvertUser(users.ToList());
        }

        public async Task<ActionResult<TokenModel>> Login(string email, string password)
        {
            email = email.Trim().ToLower();
            var res = new ActionResult<TokenModel>();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email);
            if (user != null)
            {
                if (_authService.VerifyPassword(password,user))
                {
                    res.Entity = await _authService.TokenManager(user);
                }else
                    res.AddError("Email or password is not correct.");
            }else
                res.AddError("Email or password is not correct.");
            return res;
        }

        public async Task<ActionResult> NewPassword(string newPassword, string email, int recoveryPin)
        {
            email = email.Trim().ToLower();
            var res = new ActionResult();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email && x.RecoveryPin==recoveryPin);
            if (user != null)
            {
                if (user.RecoveryPinExpireTime != null && DateTime.UtcNow.Minute - user.RecoveryPinExpireTime.Value.Minute < 30)
                {
                    user.RecoveryPinExpireTime = null;
                    _authService.PasswordManager(newPassword, user);
                    return await _userRepo.Update(user);
                }
                else
                    res.AddError("Your time has expired. Go back to 'Forgotten password'");
            }else
                res.AddError("User not found.");
            return res;
        }

        public async Task<ActionResult> RecoverPassword(string email, int recoverPin)
        {
            email = email.Trim().ToLower();
            var res = new ActionResult();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email);
            if (user != null)
            {
                if (user.RecoveryPin == recoverPin)
                {
                    user.RecoveryPinExpireTime = DateTime.UtcNow;
                }
                else
                    user.RecoveryPinExpireTime = null;
               await _userRepo.Update(user);
            }else
                res.AddError("User not found.");

            return res;
        }

        public async Task<ActionResult<TokenModel>> RefreshToken(string token)
        {
            var res = new ActionResult<TokenModel>();
            var user= await _userRepo.FindOneByPredicate(x=>x.RefreshToken==token);
            if (user != null)
            {
                if (user.RefreshTokenExpireTime!.Value>DateTime.UtcNow)
                {
                    res.Entity = await _authService.TokenManager(user);
                }else
                    res.AddError("Session expired.");
            }
            else
                res.AddError("User not found.");
            return res;
        }

        public async Task<ActionResult<TokenModel>> Register(string email, string name, string password)
        {
            var res = new ActionResult<TokenModel>();
            email = email.Trim().ToLower();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email);
            if (user == null)
            {
                user = new UserTb(name, email);
                _authService.PasswordManager(password, user);
                await _userRepo.Add(user);
                res.Entity = await _authService.TokenManager(user);
            }
            else
                res.AddError("Email is already in used.");
            return res;
        }

        public async Task<ActionResult> RemoveRoleFromUser(Guid id, string role)
        {
            role = role.Trim().ToLower();
            var assignRole=await _assignedUserRoleRepo.FindOneByPredicate(x=>x.User.Id == id && x.Role.Name==role);
            if (assignRole != null)
                return await _assignedUserRoleRepo.Delete(assignRole);
            return FailActionResult($"This user does not have role ({role})");
        }

        private static ActionResult FailActionResult(string error)
        {
            var res = new ActionResult();
            res.AddError(error);
            return res;
        }

        private static List<User> ConvertUser(List<UserTb> users)
        {
            List<User> result = new();
            foreach (var user in users)
            {
                result.Add(user);
            }
            return result;
        }
        private static int RandomPin()
        {
            var random = new Random();
            return random.Next(10000, 99999);
        }

        public async Task<ActionResult> HardDeleteRange(List<Guid> ids)
        {
           return await _userRepo.DeleteRange(ids);
        }
    }
}
