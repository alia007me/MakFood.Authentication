using MakFood.FBI.Utils;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MakFood.FBI.Contracts
{
    public class Redis : IRedis
    {
        private readonly IDatabase _redis;

        public Redis(IOptions<RedisOptions> options)
        {
            var mux = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
            _redis = mux.GetDatabase();
        }
        public async Task<bool> CheckKeyExistanceInRedis(string key)
        {
            return await _redis.KeyExistsAsync(key);
        }
    }
}
