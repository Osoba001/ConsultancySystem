using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Messages.DTOs
{
    public class DeleteDTO
    {
        public DeleteDTO(Guid id)
        {
            Id = id;
        }

        Guid Id { get; set; }
    }
}
