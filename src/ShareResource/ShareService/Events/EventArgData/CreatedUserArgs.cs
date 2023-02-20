

using ShareServices.Constant;

namespace ShareServices.Events.EventArgData
{
    public class CreatedUserArgs : EventArgs
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }
    }

}
