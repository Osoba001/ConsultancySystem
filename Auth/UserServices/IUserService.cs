using Auth.Models;
using Auth.Response;

namespace Auth.UserServices
{
    public interface IUserService
    {
        Task<ActionResult<TokenModel>> Register(string email, string name,  string password);
        Task<ActionResult<TokenModel>> Login(string email, string password);
        Task<int> ForgottenPassword(string email);
        Task<ActionResult> RecoverPassword(string email,int recoverPin);
        Task<ActionResult> NewPassword(string newPassword, string email, int recoverPin);
        Task<ActionResult> ChangePassword(Guid id, string oldPassword, string newPassword);
        Task<ActionResult<TokenModel>> RefreshToken(string token);
        Task<ActionResult> HardDeleteUser(Guid id);
        Task<ActionResult> FalseDeleteUser(Guid id);
        Task<List<User>> UsersByRoles(string role);
        Task<List<User>> AllUsers();
        Task<List<User>> GetFalseDeletedUsers(int days =0);
        Task<ActionResult> AddRoleToUser(Guid id,string role);
        Task<ActionResult> RemoveRoleFromUser(Guid id, string role);
        Task<ActionResult> UndoFalseDelete(Guid id);
        Task<ActionResult> HardDeleteRange(List<Guid> ids);

    }
}
