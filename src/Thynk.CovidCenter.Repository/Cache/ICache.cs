using System.Threading.Tasks;

namespace Thynk.CovidCenter.Repository.Cache
{
    public interface ICache
    {
        string GetValue(string key);
        Task<string> GetValueAsync(string key);
        void SetValue(string key, string value);
        Task SetValueAsync(string key, string value);
        void SetValue(string key, string value, int ttl);
        Task SetValueAsync(string key, string value, int ttl);
        Task<bool> RemoveKeyAsync(string key);
        bool RemoveKey(string key);
    }
}
