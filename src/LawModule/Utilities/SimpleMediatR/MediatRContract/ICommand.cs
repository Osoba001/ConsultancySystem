using Utilities.ActionResponse;

namespace SimpleMediatR.MediatRContract
{
    public interface ICommand
    {
        ActionResult Validate();
    }

}   
