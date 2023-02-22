using Auth.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTO;

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

        [HttpPost("client")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDTO user)
        {
            var resp = await _userService.RegisterClient(user);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [HttpPost("lawyer")]
        public async Task<IActionResult> RegisterLawyer([FromBody] CreateUserDTO user)
        {
            var resp = await _userService.RegisterLawyer(user);
            return SetTokenHeaderAndReturnAction(resp);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var resp = await _userService.Login(login);
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

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            var res = await _userService.ChangePassword(changePassword);
            return ReturnAction(res);
        }

        [HttpPost("forgotten-password")]
        public async Task<IActionResult> ForgottonPassword(string email)
        {
            var res = await _userService.ForgottenPassword(email);
            return ReturnAction(res);
        }

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
