using LCS.Domain.Repositories;
using LCS.Domain.Response;
using SimpleMediatR.MediatRContract;

namespace LCS.Application.Commands.Department
{
    public record DeleteDepartment(Guid Id) : ICommand
    {
        public ActionResult Validate()
        {
            return new ActionResult();
        }
    }

    public class DeleteDepartmentHandler : ICommandHandler<DeleteDepartment>
    {
        public async Task<ActionResult> HandleAsync(DeleteDepartment command, IRepoWrapper repo, CancellationToken cancellationToken = default)
        {
            return await repo.DepartmentRepo.Delete(command.Id);
        }
    }
}
