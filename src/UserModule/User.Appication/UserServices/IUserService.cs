using User.Application.DTO;
using User.Application.Entities;
using Utilities.ActionResponse;

namespace Auth.UserServices
{
    public interface IUserService
    {
        
        Task<ActionResult<TokenModel>> RegisterClient(CreateUserDTO user);
        Task<ActionResult<TokenModel>> RegisterLawyer(CreateUserDTO user);
        Task<ActionResult<TokenModel>> Login(LoginDTO login);
        Task<ActionResult> UpdateUser(UpdateUserDTO user);
        Task<ActionResult> UpdateLocation(UpdateLocationDTO locationDto);
        Task<ActionResult> ForgottenPassword(string email);
        Task<ActionResult> RecoverPassword(ConfirmPinDTO confirmPin);
        Task<ActionResult> NewPassword(NewPasswordDTO newPassword);
        Task<ActionResult> ChangePassword(ChangePasswordDTO changePassword);
        Task<ActionResult<TokenModel>> RefreshToken(string token);
        Task<ActionResult> HardDeleteUser(Guid id);
        Task<ActionResult> FalseDeleteUser(Guid id);
        Task<List<UserResponse>> AllUsers();
        Task<List<UserResponse>> GetFalseDeletedUsers(int days =0);
        Task<ActionResult> UndoFalseDelete(Guid id);
        Task<ActionResult> HardDeleteRange(List<Guid> ids);
        Task<ActionResult> UploadProfilePicture(UploadProfilePictureDTO profilePictureDTO);
    }
}
