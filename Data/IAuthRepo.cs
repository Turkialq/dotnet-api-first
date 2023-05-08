using dotnet_api_first.models;


namespace dotnet_api_first.Data
{
    public interface IAuthRepo
    {
        Task<ServiceRespinse<int>> Register(User user, string password);
        Task<ServiceRespinse<string>> Login(string userName, string password);
        Task<bool> UserExists(string userName);

    }
}