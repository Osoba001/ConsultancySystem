using User.Application.Constants;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace User.Application.DTO
{
    public record LoginDTO(string Email, string Password)
    {
        public ActionResult<TokenModel> ValidateModel()
        {
            var res = new ActionResult<TokenModel>();
            if (!Email.EmailValid())
                res.AddError("Email or password is not valid.");
            if (!Password.StringMaxLength(35))
                res.AddError("Email or password is not valid.");
            if (Password.Length < 5)
                res.AddError("Email or password is not valid.");
           
            return res;
        }
    }
  
}
