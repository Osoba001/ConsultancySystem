using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.Events
{
    public interface ILawModuleEventService
    {
        public void CreatedHandler(object? sender, CreatedUserArgs e);
        public void FalseDeletedHandler(object? sender, UserIdArgs e);
        public void HardDeletedHandler(object? sender, UserIdArgs e);
        public void UndoFalsedHandler(object? sender, UserIdArgs e);
    }
}
