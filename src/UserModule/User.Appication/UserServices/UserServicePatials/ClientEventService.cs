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
    public partial class UserService : IClientEventService
    {
        public event EventHandler<CreatedUserArgs>? CreatedClient;
        protected virtual void OnCreateClient(UserTb user)
        {
            CreatedClient?.Invoke(this, new CreatedUserArgs { Email = user.Email, Id = user.Id, FirstName = user.FirstName });
        }

        public event EventHandler<UserIdArgs>? HardDeletedClient;
        protected virtual void OnHardDeletedClient(Guid id)
        {
            HardDeletedClient?.Invoke(this, new UserIdArgs { Id = id });
        }

        public event EventHandler<UserIdArgs>? FalseDeletedClient;
        protected virtual void OnFalseDeletedClient(Guid id)
        {
            FalseDeletedClient?.Invoke(this, new UserIdArgs { Id = id });
        }

        public event EventHandler<UserIdArgs>? UndoFalseDeletedClient;
        protected virtual void OnUndoFalseDeletedClient(Guid id)
        {
            UndoFalseDeletedClient?.Invoke(this, new UserIdArgs { Id = id });
        }
    }
}
