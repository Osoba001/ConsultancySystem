using Auth.UserServices;
using AuthLibrary.WenApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LCS.WebApi.Controllers.AuthModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand user)
        {
            var resp = await _userService.Register(user.Email, user.UserName, user.Password);
            if (resp.IsSuccess && resp.Entity!.RefreshToken != null)
            {
                Response.Cookies.Append("refreshToken", resp.Entity.RefreshToken, new CookieOptions { HttpOnly = true });
                return Ok(resp.Entity.AccessToken);
            }
            return BadRequest(resp.FistError);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand login)
        {
            var resp = await _userService.Login(login.Email, login.Password);
            if (resp.IsSuccess && resp.Entity!.RefreshToken != null)
            {
                Response.Cookies.Append("refreshToken", resp.Entity.RefreshToken, new CookieOptions { HttpOnly = true });
                return Ok(resp.Entity.AccessToken);
            }
            return BadRequest(resp.FistError);
        }
        [HttpGet("refresh-accessToken")]
        public async Task<IActionResult> ResfreshAccessToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken != null)
            {
                var res = await _userService.RefreshToken(refreshToken);
                if (res.IsSuccess)
                    return Ok(res.Entity!.AccessToken);
                else
                    return BadRequest(res.FistError);
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

        [HttpPost("user-role")]
        public async Task<IActionResult> AddRole([FromQuery] string userRole)
        {
            var resp = await _userService.AddRole(userRole);
            return ReturnAction(resp);
        }
        [HttpDelete("role")]
        public async Task<IActionResult> RemoveRole([FromQuery] string userRole)
        {
            var resp = await _userService.RemoveRole(userRole);
            return ReturnAction(resp);
        }

        [HttpGet("user-roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await _userService.GetAllRoles());
        }

       

        [HttpDelete("false-delete")]
        public async Task<IActionResult> FalseDeleteUser(Guid userId)
        {
            var res = await _userService.FalseDeleteUser(userId);
            return ReturnAction(res);
        }

        [HttpPut("undo-False-delete")]
        public async Task<IActionResult> UndoFalseDelete(Guid userId)
        {
            var res = await _userService.UndoFalseDelete(userId);
            return ReturnAction(res);
        }

        [HttpDelete("hard-delete")]
        public async Task<IActionResult> HardDeleteUser(Guid userId)
        {
            var res = await _userService.HardDeleteUser(userId);
            return ReturnAction(res);
        }

        [HttpGet("User-by-role")]
        public async Task<IActionResult> GetUserByRole(string role)
        {
            return Ok(await _userService.UsersByRoles(role));
        }

        [HttpPut("add-role-to-user")]
        public async Task<IActionResult> AddRoleToUser(Guid userId, string role)
        {
            var res = await _userService.AddRoleToUser(userId, role);
            return ReturnAction(res);
        }
        [HttpPut("remove-role-from-user")]
        public async Task<IActionResult> RemoveRoleFromUser(Guid userId, string role)
        {
            var res = await _userService.RemoveRoleFromUser(userId, role);
            return ReturnAction(res);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePassword)
        {
            var res = await _userService.ChangePassword(changePassword.UserId, changePassword.OldPassword, changePassword.NewPassword);
            return ReturnAction(res);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgottonPassword(string email)
        {
            var pin = await _userService.ForgottenPassword(email);
            if (pin > 0)
            {
                //send pin to the user email or sms.
                // for testing purpose i will return pin
                return Ok(pin);
            }
            else
                return BadRequest("Invalid email");
        }

        [HttpPost("confirm-password-recovery-pin")]
        public async Task<IActionResult> PasswordRecovery([FromBody] ConfirmPinCommand confirmPin)
        {
            var res = await _userService.RecoverPassword(confirmPin.Email, confirmPin.RecoveryPin);
            return ReturnAction(res);
        }

        [HttpPut("new-password")]
        public async Task<IActionResult> EnterNewPassword([FromBody] NewPasswordCommand newPassword)
        {
            var res = await _userService.NewPassword(newPassword.Password, newPassword.Email, newPassword.RecoveryPin);
            return ReturnAction(res);
        }

        private IActionResult ReturnAction(Auth.Response.ActionResult res)
        {
            if (res.IsSuccess)
                return Ok();
            else
                return BadRequest(res.FistError);
        }
    }
}
