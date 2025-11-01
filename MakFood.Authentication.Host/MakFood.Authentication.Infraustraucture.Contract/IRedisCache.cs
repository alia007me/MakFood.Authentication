using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Contract
{
    public interface IRedisCache
    {
        Task AddToRedis(string key, string value, TimeSpan expiredTime);
        Task RemoveFromRedis(string key);
        Task<bool> CheckKeyExistanceInRedis(string key);
        Task<string> GetValueInRedis(string key);
    }
}
