using MakFood.Authentication.Infraustraucture.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Context.Redis
{
    public class RedisCache : IRedisCache
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCache(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        
        public async Task AddToRedis(string key, string value, TimeSpan expiredTime)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value , expiredTime);
        }

        public async Task<bool> CheckKeyExistanceInRedis(string key)
        {
            var db = _redis.GetDatabase();
            return await db.KeyExistsAsync(key);
        }

        public async Task<string> GetValueInRedis(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task RemoveFromRedis(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
