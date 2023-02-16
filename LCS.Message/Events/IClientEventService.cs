namespace ShareServices.Events
{
    public interface IClientEventService
    {
        event EventHandler<CreatedUserArgs>? CreatedClient;
        event EventHandler<UserIdArgs>? HardDeletedClient; 
        event EventHandler<UserIdArgs>? FalseDeletedClient; 
        event EventHandler<UserIdArgs>? UndoFalseDeletedClient; 
    }
}
