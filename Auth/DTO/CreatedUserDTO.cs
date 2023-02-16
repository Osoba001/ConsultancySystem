using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Entities;

namespace User.Application.DTO
{
    public class CreatedUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public static implicit operator CreatedUserDTO(UserTb tb)
        {
            return new CreatedUserDTO
            {
                Id = tb.Id,
                FirstName = tb.FirstName,
                Email = tb.Email,
            };
        }
    }
}
