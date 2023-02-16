using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Constants;

namespace User.Application.DTO
{
    public record class UpdateUserDTO(string MiddleName, string LastName, string PhoneNo, Guid Id, DateTime DOB, Gender Gender);

}
