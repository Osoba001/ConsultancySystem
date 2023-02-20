using Law.Domain.Repositories;
using SimpleMediatR.MediatRContract;
using Utilities.ActionResponse;

namespace Law.Application.Commands.DepartmentC
{
    public record DeleteDepartment : ICommand
    {
        public Guid Id { get; set; }
        public ActionResult Validate() => new();
    }


    public class DeleteDepartmentHandler : ICommandHandler<DeleteDepartment>
    {
        public async Task<ActionResult> HandleAsync(DeleteDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.DepartmentRepo.Delete(command.Id);
        }
    }
}
