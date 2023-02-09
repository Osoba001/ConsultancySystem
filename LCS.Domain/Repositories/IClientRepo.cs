using LCS.Domain.Entities;
using LCS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Domain.Repositories
{
    public interface IClientRepo: IBaseRepo<ClientTB>
    {
        List<Client> Convertlist(List<ClientTB> listTB);
        public Task FalseDelete(Guid id);
        public Task UndoFalseDelete(Guid id);
    }
}
