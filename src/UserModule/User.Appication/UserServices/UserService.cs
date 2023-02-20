using Auth.UserServices;
using ShareServices.ASMessages;
using ShareServices.Constant;
using ShareServices.Events;
using ShareServices.RedisMsgDTO;
using User.Application.AuthServices;
using User.Application.DTO;
using User.Application.Entities;
using User.Application.Repository;
using Utilities.ActionResponse;
using Utilities.UtlityExtensions;

namespace User.Application.UserServices
{
    public partial class UserService : IUserService
    {
        private readonly ILawModuleEventService _lawModuleEvent;
        private readonly IUserRepo _userRepo;
        private readonly IAuthService _authService;
        private readonly IRedisMsg _redisMsg;

        public UserService(IUserRepo userRepo, IAuthService authService,IRedisMsg redisMsg, ILawModuleEventService lawModuleEvent)
        {
            _userRepo = userRepo;
            _authService = authService;
            _redisMsg = redisMsg;
            _lawModuleEvent = lawModuleEvent;
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
                    CreatedUser += _lawModuleEvent.CreatedHandler;
                    OnFalseDeletedUser(user);
                    CreatedUser-= _lawModuleEvent.CreatedHandler;
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
                    UndoFalseDeletedUser+= _lawModuleEvent.UndoFalsedHandler;
                    OnUndoFalseDeletedUser(user);
                    UndoFalseDeletedUser -= _lawModuleEvent.UndoFalsedHandler;
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
                    HardDeletedUser += _lawModuleEvent.HardDeletedHandler;
                    OnHardDeletedUser(user);
                    HardDeletedUser -= _lawModuleEvent.HardDeletedHandler;
                }
                return res;
            }
            return FailActionResult("User not found.");
        }

        public async Task<ActionResult> ForgottenPassword(string email)
        {
            email = email.Trim().ToLower();
            var user = await _userRepo.FindOneByPredicate(x => x.Email.ToLower() == email);
            if (user != null)
            {
                user.RecoveryPin = RandomPin();
                user.RecoveryPinExpireTime = DateTime.UtcNow.AddMinutes(20);
               var rs= await _userRepo.Update(user);
                if (!rs.IsSuccess)
                    await  _redisMsg.PublishAsync(new RecoveringPasswordDTO { Email = user.Email, Name = user.FirstName, Pin = user.RecoveryPin }, "forgetPassword");
                return rs;
            }
            var res = new ActionResult();
            res.AddError("User is not found.");
            return res;
        }

        public async Task<List<UserResponse>> AllUsers()
        {
            return ConvertUser(await _userRepo.GetAll());
        }
        public async Task<List<UserResponse>> GetFalseDeletedUsers(int days = 0)
        {
            return ConvertUser(await _userRepo
                .IgnorQueryFilter(x => x.IsDeleted && x.DeletedDate != null && DateTime.UtcNow.Day - x.DeletedDate.Value.Day >= days));
        }

        public async Task<ActionResult<TokenModel>> Login(LoginDTO login)
        {
            var validate = login.ValidateModel();
            if (!validate.IsSuccess)
                return validate;
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
                if (user.RecoveryPinExpireTime!=null && user.RecoveryPinExpireTime>DateTime.UtcNow)
                {
                    if (user.RecoveryPin == confirmPin.RecoveryPin)
                    {
                        user.RecoveryPinExpireTime = null;
                        res.AddError("Invalid recovering code.");
                    }
                    await _userRepo.Update(user);
                }else
                    res.AddError("Invalid session.");
            }
            else
                res.AddError("Invalid Email.");
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
            }else
                res.AddError("User is not found.");
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
                var res= await _userRepo.AddAndReturn(user);
                if (res.IsSuccess)
                {
                    CreatedUser += _lawModuleEvent.CreatedHandler;
                    OnCreatedUser(user);
                    CreatedUser -= _lawModuleEvent.CreatedHandler;
                }
                return res;
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

        private static List<UserResponse> ConvertUser(List<UserTb> users)
        {
            List<UserResponse> result = new();
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
            var validate = user.Validate();
            if (!validate.IsSuccess)
                return validate;
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

        public async Task<ActionResult> UploadProfilePicture(UploadProfilePictureDTO profilePictureDTO)
        {
            var validate = profilePictureDTO.Validate();
            if (!validate.IsSuccess)
                return validate;
            var user = await _userRepo.FindById(profilePictureDTO.userId);
            if (user != null)
                return FailActionResult("User is not found.");
            
            user.ImageArray = profilePictureDTO.File.ResizeImage(170, 170).BitmapToByteArray();
            return await _userRepo.Update(user);
        }
    }
}


