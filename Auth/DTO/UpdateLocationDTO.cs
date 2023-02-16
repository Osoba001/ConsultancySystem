using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTO
{
    public record UpdateLocationDTO(Guid Id, string Location, string State);
    
}
