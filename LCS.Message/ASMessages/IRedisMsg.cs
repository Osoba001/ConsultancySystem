using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareServices.ASMessages
{
    public interface IRedisMsg
    {
        ValueTask<bool> PublishAsync<T>(T record, string channel);

        void SubscribeAsync<T>(string channel, Action<T> action);
    }
}
