using ShareServices.Events.EventArgData;

namespace ShareServices.Events
{
    public interface ILawyerEventService
    {
        event EventHandler<CreatedUserArgs>? CreatedLawyer;
        event EventHandler<UserIdArgs>? HardDeletedLawyer;
        event EventHandler<UserIdArgs>? FalseDeletedLawyer;
        event EventHandler<UserIdArgs>? UndoFalseDeletedLawyer;
    }
}
