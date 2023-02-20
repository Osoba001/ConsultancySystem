using ShareServices.Events;
using ShareServices.Events.EventArgData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Entities;

namespace User.Application.UserServices
{
    public partial class UserService : IUserServiceEvent
    {
       
        public event EventHandler<CreatedUserArgs>? CreatedUser;
        protected virtual void OnCreatedUser(UserTb user)
        {
            CreatedUser?.Invoke(this, new CreatedUserArgs { Email = user.Email, Id = user.Id, FirstName = user.FirstName,Role=user.Role });
        }
        public event EventHandler<UserIdArgs>? HardDeletedUser;
        protected virtual void OnHardDeletedUser(UserTb user)
        {
            HardDeletedUser?.Invoke(this, new UserIdArgs { Id = user.Id,Role=user.Role });
        }
        public event EventHandler<UserIdArgs>? FalseDeletedUser;
        protected virtual void OnFalseDeletedUser(UserTb user)
        {
            FalseDeletedUser?.Invoke(this, new UserIdArgs { Id = user.Id, Role = user.Role });
        }
        public event EventHandler<UserIdArgs>? UndoFalseDeletedUser;
        protected virtual void OnUndoFalseDeletedUser(UserTb user)
        {
            UndoFalseDeletedUser?.Invoke(this, new UserIdArgs { Id = user.Id, Role = user.Role });
        }
    }
}
