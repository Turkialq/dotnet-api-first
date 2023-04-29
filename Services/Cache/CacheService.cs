using System.Text.Json;
using StackExchange.Redis;

namespace dotnet_api_first.Services.Chache
{
    public class CacheService : ICacheService
    {
        private IDatabase _cacheDB;
        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDB = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cacheDB.StringGet(key);

            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);
            return default!;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDB.StringSet(key, JsonSerializer.Serialize(value), expirTime);

        }
        public object RemoveData(string key)
        {
            var _exit = _cacheDB.KeyExists(key);

            if (_exit)
                return _cacheDB.KeyDelete(key);
            return false;
        }
    }
}