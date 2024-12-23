namespace SympliSearch.Application.Interfaces
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, TimeSpan duration);
        bool TryGet<T>(string key, out T? value);
    }
}
