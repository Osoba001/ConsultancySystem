
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Constants;
using Utilities.ActionResponse;
using Utilities.RegexFormatValidations;

namespace User.Application.DTO
{
    public record class UpdateUserDTO(string MiddleName, string LastName, string PhoneNo, Guid Id, DateTime DOB, Gender Gender)
    {
        public ActionResult Validate()
        {
            var res=new ActionResult();
            if (!LastName.StringMaxLength())
                res.AddError("Last name length is out of range.");
            if (!PhoneNo.PhoneNoValid())
                res.AddError("Invalid phone number.");
            if (!DOB.PastDate(DateTime.Now.AddYears(-150)))
                res.AddError("Invalid date of birth.");
            if ((int)Gender <0||(int)Gender>1)
                res.AddError("Invalid gender.");
            return res;
        }
    }

}
