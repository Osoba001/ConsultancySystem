using Law.Domain.Repositories;
using ShareServices.Events;
using ShareServices.Events.EventArgData;

namespace Law.Application.Commands.ClientC
{
    public class DeleteClient
    {
        private readonly IClientEventService _clientEvent;
        private readonly IRepoWrapper _repo;

        public DeleteClient(IClientEventService clientEvent, IRepoWrapper repo)
        {
            _clientEvent = clientEvent;
            _repo = repo;
            _clientEvent.HardDeletedClient += DeleteClientHandler;
        }

        private async void DeleteClientHandler(object? sender, UserIdArgs e)
        {
            var res = await _repo.ClientRepo.Delete(e.Id);
            _clientEvent.HardDeletedClient -= DeleteClientHandler;
            if (!res.IsSuccess)
                throw new Exception(res.Errors());
        }
       

    }
}
