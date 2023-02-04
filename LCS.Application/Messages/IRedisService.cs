using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Application.Messages
{
    public interface IRedisService
    {
        Task SetRecordAsync<T>(string recordId, T data,
           TimeSpan? absoluteExpireTime = null,
           TimeSpan? unusedExpireTime = null);

        Task<T?> GetRecordAsync<T>(string recordId);

        ValueTask<bool> PublishAsync<T>(T record, string channel);

        Task<T> SubscribeAsync<T>(string channel);
    }
}
