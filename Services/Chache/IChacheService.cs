namespace dotnet_api_first.Services.Chache
{
    public interface IChacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);

    }
}