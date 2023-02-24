using Auth.UserServices;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareServices.Events;
using User.Application.DTO;

namespace LCS.WebApi.Controllers.AuthModule
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILawModuleEventService _lawModuleEvent;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, ILawModuleEventService lawModuleEvent,IConfiguration config)
        {
            _userService = userService;
            _lawModuleEvent = lawModuleEvent;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("client")]
        public async Task<IActionResult> RegisterClient([FromBody] CreateUserDTO user)
        {
            _userService.CreatedUser += _lawModuleEvent.CreatedHandler;
            var resp = await _userService.RegisterClient(user);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [AllowAnonymous]
        [HttpPost("lawyer")]
        public async Task<IActionResult> RegisterLawyer([FromBody] CreateUserDTO user)
        {
            _userService.CreatedUser += _lawModuleEvent.CreatedHandler;
            var resp = await _userService.RegisterLawyer(user);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [AllowAnonymous]
        [HttpPost("Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateUserDTO user, string AdminSecret)
        {
            string adminSecret = GetAdminSecret();
            if (AdminSecret != adminSecret)
                return BadRequest("Not allow.");
            var resp = await _userService.RegisterAdmin(user);
            return SetTokenHeaderAndReturnAction(resp);
        }

        private string GetAdminSecret()
        {
            string vaultUri = _config.GetSection("VaultUri").Value;
            var client = new SecretClient(vaultUri: new Uri(vaultUri), credential: new DefaultAzureCredential());
            KeyVaultSecret adminSecret = client.GetSecret("AdminSecret");
            return adminSecret.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var resp = await _userService.Login(login);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [AllowAnonymous]
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDTO login, string AdminSecret)
        {
            string adminSecret = GetAdminSecret();
            if (AdminSecret != adminSecret)
                return BadRequest("Not allow.");
            var resp = await _userService.AdminLogin(login);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO user)
        {
            var resp=await _userService.UpdateUser(user);
            return ReturnAction(resp);
        }
        [HttpPut("location")]
        public async Task<IActionResult> UpdateUserLocation([FromBody] UpdateLocationDTO location)
        {
            var resp = await _userService.UpdateLocation(location);
            return ReturnAction(resp);
        }
        [HttpGet("refresh-accessToken")]
        public async Task<IActionResult> ResfreshAccessToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
            {
                var res = await _userService.RefreshToken(refreshToken);
                return SetTokenHeaderAndReturnAction(res);
            }
            else
                return BadRequest("Refresh to is null.");
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.AllUsers());
        }

        [HttpGet("False-deleted-users")]
        public async Task<IActionResult> GetFalseDeletedUsers()
        {
            return Ok(await _userService.GetFalseDeletedUsers());
        }

        [HttpDelete("false-delete")]
        public async Task<IActionResult> FalseDeleteUser(Guid userId)
        {
            _userService.FalseDeletedUser += _lawModuleEvent.FalseDeletedHandler;
            var res = await _userService.FalseDeleteUser(userId);
            return ReturnAction(res);
        }

        [HttpPut("undo-False-delete")]
        public async Task<IActionResult> UndoFalseDelete(Guid userId)
        {
            _userService.UndoFalseDeletedUser += _lawModuleEvent.UndoFalsedHandler;
            var res = await _userService.UndoFalseDelete(userId);
            return ReturnAction(res);
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> HardDeleteUser(Guid userId)
        {
            _userService.HardDeletedUser += _lawModuleEvent.HardDeletedHandler;
            var res = await _userService.HardDeleteUser(userId);
            return ReturnAction(res);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            var res = await _userService.ChangePassword(changePassword);
            return ReturnAction(res);
        }
        [AllowAnonymous]
        [HttpPost("forgotten-password")]
        public async Task<IActionResult> ForgottonPassword(string email)
        {
            var res = await _userService.ForgottenPassword(email);
            return ReturnAction(res);
        }
        [AllowAnonymous]
        [HttpPost("confirm-password-recovery-pin")]
        public async Task<IActionResult> PasswordRecovery([FromBody] ConfirmPinDTO confirmPin)
        {
            var res = await _userService.RecoverPassword(confirmPin);
            return ReturnAction(res);
        }

        [HttpPut("new-password")]
        public async Task<IActionResult> EnterNewPassword([FromBody] NewPasswordDTO newPassword)
        {
            var res = await _userService.NewPassword(newPassword);
            return ReturnAction(res);
        }
        [HttpPost("propfile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile photo,Guid id)
        {
            var res = await _userService.UploadProfilePicture(new UploadProfilePictureDTO(photo,id));
            return ReturnAction(res);
        }

        private IActionResult ReturnAction(Utilities.ActionResponse.ActionResult res)
        {
            if (res.IsSuccess)
                return Ok();
            else
                return BadRequest(res.ToString());
        } 
        private IActionResult SetTokenHeaderAndReturnAction(Utilities.ActionResponse.ActionResult<TokenModel> resp)
        {
            if (resp.IsSuccess)
            {
                Response.Cookies.Append("refreshToken", resp.Item.RefreshToken!, new CookieOptions { HttpOnly = true });
                return Ok(resp.Item.AccessToken);
            }
            else
                return BadRequest(resp.Errors());
        }
    }
}
