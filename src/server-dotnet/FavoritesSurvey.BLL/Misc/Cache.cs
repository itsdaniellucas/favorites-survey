using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.BLL.Misc
{
    public class Cache : ICache, IDisposable
    {
        IRedisClientsManager _mgr;
        IRedisClientAsync _redis;
        IRedisClient _redisSync;
        TimeSpan _exp;

        public Cache(IRedisClientsManager mgr)
        {
            _mgr = mgr;
            _exp = TimeSpan.FromMinutes(15);
            _redisSync = _mgr.GetClient();
        }

        private async Task InitializeRedisClient()
        {
            if (_redis == null)
                _redis = await _mgr.GetClientAsync();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            await InitializeRedisClient();
            return await _redis.GetAsync<T>(key);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await InitializeRedisClient();
            await _redis.SetAsync(key, value, _exp);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan exp)
        {
            await InitializeRedisClient();
            await _redis.SetAsync(key, value, exp);
        }

        public async void Dispose()
        {
            if(_redis != null)
                await _redis.DisposeAsync();
            _redisSync.Dispose();
            _mgr.Dispose();
        }

        public T Get<T>(string key)
        {
            return _redisSync.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            _redisSync.Set(key, value);
        }

        public void Set<T>(string key, T value, TimeSpan exp)
        {
            _redisSync.Set(key, value, exp);
        }
    }

    public interface ICache : IDisposable
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
        Task SetAsync<T>(string key, T value, TimeSpan exp);

        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, TimeSpan exp);
    }
}
