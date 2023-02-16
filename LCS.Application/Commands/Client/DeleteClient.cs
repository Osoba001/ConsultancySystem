using Law.Domain.Repositories;
using ShareServices.Events;

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
        //private readonly IRepoWrapper _repo;
        //private readonly string conString;
        //private const string Chinnel = "hardDeleteClient";
        //public DeleteClient(IRepoWrapper repo, IOptionsSnapshot<RedisConfigModel> redisConfModelOpt)
        //{
        //    _repo = repo;
        //    conString = redisConfModelOpt.Value.ConString;
        //}
        //private async void HardDeleteClientHandle(Guid id)
        //{
        //  var res= await _repo.ClientRepo.Delete(id);
        //    if (!res.IsSuccess)
        //        throw new Exception(res.ToString());
        //}
        //private void SubscribeToHardDeleteClient()
        //{
        //    var connection = ConnectionMultiplexer.Connect(conString);
        //     connection.GetSubscriber().Subscribe(Chinnel, async (channel, message) =>
        //    {
        //        if (message.IsNull)
        //            throw new Exception($"The Redis message subscribed to is null when trying to hard delete client.");
        //        var id = JsonSerializer.Deserialize<Guid>(message!);
        //        HardDeleteClientHandle(id);
        //    });
        //}

    }
}
