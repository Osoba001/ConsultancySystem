using ShareServices.Constant;

namespace ShareServices.Events.EventArgData
{
    public class UserIdArgs : EventArgs
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
