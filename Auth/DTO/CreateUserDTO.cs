using User.Application.Constants;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace User.Application.DTO
{
    public record CreateUserDTO(string Email, string FirstName, string Password, DateTime DOB, Gender Gender)
    {
        public ActionResult<TokenModel> ValidateModel()
        {
            var res = new ActionResult<TokenModel>();
            if (!Email.EmailValid())
                res.AddError("Invalid email.");
            if (!FirstName.StringMaxLength())
                res.AddError("First name length is out of range.");
            if (Password.StringMaxLength(35))
                res.AddError("Password is too long.");
            if (Password.Length < 5)
                res.AddError("Password is too short.");
            if (!DOB.PastDate(DateTime.Now.AddYears(-150)))
                res.AddError("Date of birth is out of range.");
            if (Gender != Gender.Male || Gender != Gender.Female)
                res.AddError("Invalid gender");
            return res;
        }
    }

}
