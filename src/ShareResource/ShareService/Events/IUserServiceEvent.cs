using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.Events
{
    public interface IUserServiceEvent
    {
        event EventHandler<CreatedUserArgs>? CreatedUser;
        event EventHandler<UserIdArgs>? HardDeletedUser;
        event EventHandler<UserIdArgs>? FalseDeletedUser;
        event EventHandler<UserIdArgs>? UndoFalseDeletedUser;
    }
}
