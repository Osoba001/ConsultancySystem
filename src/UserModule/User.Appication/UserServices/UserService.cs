using Auth.UserServices;
using User.Application.AuthServices;
using User.Application.Constants;
using User.Application.DTO;
using User.Application.Entities;
using User.Application.Repository;
using Utilities.ActionResponse;

namespace User.Application.UserServices
{
    public partial class UserService : IUserService
    {

        private readonly IUserRepo _userRepo;
        private readonly IAuthService _authService;



        public UserService(IUserRepo userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        public async Task<ActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            var res = new ActionResult();
            var user = await _userRepo.FindById(changePassword.UserId);
            if (user != null)
            {
                if (_authService.VerifyPassword(changePassword.OldPassword, user))
                {
                    _authService.PasswordManager(changePassword.NewPassword, user);
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
            var user = await _userRepo.FindOneByPredicate(x => x.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedDate = DateTime.UtcNow;
                var res = await _userRepo.Update(user);
                if (res.IsSuccess)
                {
                    if (user.Role == Role.Client)
                        // _redisMsg.PublishAsync(user.Id, "falseDeleteClient");
                        OnFalseDeletedClient(id);
                    else
                        OnFalseDeletedLawyer(user.Id);
                }
                return res;
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
                var res = await _userRepo.Update(user);
                if (res.IsSuccess)
                {
                    if (user.Role == Role.Client)
                        // await _redisMsg.PublishAsync(user.Id, "undoFalseDeleteClient");
                        OnUndoFalseDeletedClient(user.Id);
                    else
                        OnUndoFalseDeletedLawyer(user.Id);
                }
                return res;
            }
            return FailActionResult("User not found.");
        }
        public async Task<ActionResult> HardDeleteUser(Guid id)
        {
            var user = await _userRepo.FindById(id);
            if (user != null)
            {
                var res = await _userRepo.Delete(user);
                if (res.IsSuccess)
                {
                    if (user.Role == Role.Client)
                        // await _redisMsg.PublishAsync(user.Id, "hardDeleteClient");
                        OnHardDeletedClient(user.Id);
                    else
                        OnHardDeletedLawyer(user.Id);
                }
                return res;
            }
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

        public async Task<List<UserTb>> AllUsers()
        {
            return ConvertUser(await _userRepo.GetAll());
        }
        public async Task<List<UserTb>> GetFalseDeletedUsers(int days = 0)
        {
            return ConvertUser(await _userRepo
                .IgnorQueryFilter(x => x.IsDeleted && x.DeletedDate != null && DateTime.UtcNow.Day - x.DeletedDate.Value.Day >= days));
        }

        public async Task<ActionResult<TokenModel>> Login(LoginDTO login)
        {
            var res = new ActionResult<TokenModel>();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == login.Email.Trim().ToLower());
            if (user != null)
            {
                if (_authService.VerifyPassword(login.Password, user))
                {
                    res.Item = await _authService.TokenManager(user);
                }
                else
                    res.AddError("Email or password is not correct.");
            }
            else
                res.AddError("Email or password is not correct.");
            return res;
        }

        public async Task<ActionResult> NewPassword(NewPasswordDTO newPassword)
        {
            var res = new ActionResult();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == newPassword.Email.ToLower().Trim() && x.RecoveryPin == newPassword.RecoveryPin);
            if (user != null)
            {
                if (user.RecoveryPinExpireTime != null && DateTime.UtcNow.Minute - user.RecoveryPinExpireTime.Value.Minute < 30)
                {
                    user.RecoveryPinExpireTime = null;
                    _authService.PasswordManager(newPassword.Password, user);
                    return await _userRepo.Update(user);
                }
                else
                    res.AddError("Your time has expired. Go back to 'Forgotten password'");
            }
            else
                res.AddError("User not found.");
            return res;
        }

        public async Task<ActionResult> RecoverPassword(ConfirmPinDTO confirmPin)
        {
            var res = new ActionResult();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == confirmPin.Email.ToLower().Trim());
            if (user != null)
            {
                if (user.RecoveryPin == confirmPin.RecoveryPin)
                {
                    user.RecoveryPinExpireTime = DateTime.UtcNow;
                }
                else
                    user.RecoveryPinExpireTime = null;
                await _userRepo.Update(user);
            }
            else
                res.AddError("User not found.");

            return res;
        }

        public async Task<ActionResult<TokenModel>> RefreshToken(string token)
        {
            var res = new ActionResult<TokenModel>();
            var user = await _userRepo.FindOneByPredicate(x => x.RefreshToken == token);
            if (user != null)
            {
                if (user.RefreshTokenExpireTime!.Value > DateTime.UtcNow)
                {
                    res.Item = await _authService.TokenManager(user);
                }
                else
                    res.AddError("Session expired.");
            }
            else
                res.AddError("User not found.");
            return res;
        }

        public async Task<ActionResult<TokenModel>> RegisterClient(CreateUserDTO dto)
        {
            var res = dto.ValidateModel();
            if (!res.IsSuccess)
                return res;
            var result = await Register(dto, Role.Client);
            if (result.IsSuccess)
            {
                OnCreateClient(result.Item!);
                return await GenerateToken(result.Item!);
            }
            res.AddError(result.Errors());
            return res;
        }
        public async Task<ActionResult<TokenModel>> RegisterLawyer(CreateUserDTO dto)
        {
            var res = dto.ValidateModel();
            if (!res.IsSuccess)
                return res;
            var result = await Register(dto, Role.Lawyer);
            if (result.IsSuccess)
            {
                OnCreatedLawyer(result.Item!);
                return await GenerateToken(result.Item!);
            }
            res.AddError(result.Errors());
            return res;
        }

        private async Task<ActionResult<TokenModel>> GenerateToken(UserTb user)
        {
            var res = new ActionResult<TokenModel>();
            res.Item = await _authService.TokenManager(user);
            return res;
        }

        private async Task<ActionResult<UserTb>> Register(CreateUserDTO dto, Role role)
        {


            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == dto.Email.ToLower().Trim());
            if (user == null)
            {
                user = new UserTb()
                {
                    Role = role,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    Gender = dto.Gender,
                    DOB = dto.DOB
                };
                _authService.PasswordManager(dto.Password, user);
                return await _userRepo.AddAndReturn(user);
            }
            else
                return FailActionResult<UserTb>("Email is already in used.");
        }

        private static ActionResult FailActionResult(string error)
        {
            var res = new ActionResult();
            res.AddError(error);
            return res;
        }
        private static ActionResult<U> FailActionResult<U>(string error) where U : class
        {
            var res = new ActionResult<U>();
            res.AddError(error);
            return res;
        }

        private static List<UserTb> ConvertUser(List<UserTb> users)
        {
            List<UserTb> result = new();
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

        public async Task<ActionResult> UpdateUser(UpdateUserDTO user)
        {
            var res = await _userRepo.FindById(user.Id);
            if (res != null)
            {
                res.DOB = user.DOB;
                res.Gender = user.Gender;
                res.LastName = user.LastName;
                res.MiddleName = user.MiddleName;
                res.PhoneNo = user.PhoneNo;
                return await _userRepo.Update(res);
            }
            else
                return FailActionResult("User is not found.");
        }

        public async Task<ActionResult> UpdateLocation(UpdateLocationDTO locationDto)
        {
            var res = await _userRepo.FindById(locationDto.Id);
            if (res != null)
            {
                res.Location = locationDto.Location;
                res.State = locationDto.State;
                return await _userRepo.Update(res);
            }
            else
                return FailActionResult("User is not found.");
        }
    }


}
