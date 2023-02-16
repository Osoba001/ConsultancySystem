using Law.Domain.Repositories;
using ShareServices.Events;

namespace Law.Application.Commands.ClientC
{
    public class FalseDeleteClient
    {
        private readonly IClientEventService _clientEvent;
        private readonly IRepoWrapper _repo;

        public FalseDeleteClient(IClientEventService clientEvent, IRepoWrapper repo)
        {
            _clientEvent = clientEvent;
            _repo = repo;
            _clientEvent.FalseDeletedClient += FalseDeleteClientHandler;
        }

        private async void FalseDeleteClientHandler(object? sender, UserIdArgs e)
        {
            var user = await _repo.ClientRepo.GetById(e.Id);
            if (user != null)
            {
                user.IsDelete = true;
                var res = await _repo.ClientRepo.Update(user);
                _clientEvent.FalseDeletedClient -= FalseDeleteClientHandler;
                if (!res.IsSuccess)
                    throw new Exception(res.Errors());
            }
            else
                throw new Exception("Client you're trying to delete is not found.");
        }
        //private readonly IRepoWrapper _repo;
        //private readonly string conString;
        //private const string Chinnel = "falseDeleteClient";
        //public FalseDeleteClient(IRepoWrapper repo, IOptionsSnapshot<RedisConfigModel> redisConfModelOpt)
        //{
        //    _repo = repo;
        //    conString = redisConfModelOpt.Value.ConString;
        //}
        //private void SubscribeToDeleteClient()
        //{
        //    var connection = ConnectionMultiplexer.Connect(conString);
        //    connection.GetSubscriber().Subscribe(Chinnel, async (channel, message) =>
        //    {
        //        if (message.IsNull)
        //            throw new Exception($"The Redis message subscribed to is null when trying to false delete client.");
        //        var id = JsonSerializer.Deserialize<Guid>(message!);
        //        HandleAsync(id);
        //    });
        //}
        //public async void HandleAsync(Guid id)
        //{
        //    var user = await _repo.ClientRepo.GetById(id);
        //    if (user != null)
        //    {
        //        user.IsDelete = true;
        //      var res= await _repo.ClientRepo.Update(user);
        //        if (!res.IsSuccess)
        //            throw new Exception(res.ToString());
        //    }
        //    else
        //        throw new Exception($"user you're trying to false delete is not found.");
        //}
    }



}
