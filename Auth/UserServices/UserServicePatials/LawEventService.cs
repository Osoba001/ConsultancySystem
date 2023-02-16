using ShareServices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Entities;

namespace User.Application.UserServices
{
    public partial class UserService : ILawyerEventService
    {
        public event EventHandler<CreatedUserArgs>? CreatedLawyer;
        protected virtual void OnCreatedLawyer(UserTb user)
        {
            CreatedLawyer?.Invoke(this, new CreatedUserArgs { Email = user.Email, Id = user.Id, FirstName = user.FirstName });
        }

        public event EventHandler<UserIdArgs>? HardDeletedLawyer;
        protected virtual void OnHardDeletedLawyer(Guid id)
        {
            HardDeletedLawyer?.Invoke(this, new UserIdArgs { Id = id });
        }

        public event EventHandler<UserIdArgs>? FalseDeletedLawyer;
        protected virtual void OnFalseDeletedLawyer(Guid id)
        {
            FalseDeletedLawyer?.Invoke(this, new UserIdArgs { Id = id });
        }

        public event EventHandler<UserIdArgs>? UndoFalseDeletedLawyer;
        protected virtual void OnUndoFalseDeletedLawyer(Guid id)
        {
            UndoFalseDeletedLawyer?.Invoke(this, new UserIdArgs { Id = id });
        }
    }
}
