using Microsoft.Extensions.Options;
using ShareServices.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace ShareServices.ASMessages
{
    public class Messages : IRedisMsg
    {
        private readonly string conString;
        public Messages(IOptions<RedisConfigModel> redisModel)
        {
            conString = redisModel.Value.ConString;
        }
        public async ValueTask<long> PublishAsync<T>(T record, string channel)
        {
            var jsonData = JsonSerializer.Serialize(record);
            var connection = ConnectionMultiplexer.Connect(conString);
            return await connection.GetSubscriber().PublishAsync(channel, jsonData);
        }
    }
}
