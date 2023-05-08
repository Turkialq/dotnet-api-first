namespace dotnet_api_first.Services.BackgroundJobs
{
    public interface IBackground
    {
        void SyncData();
        void UpdateDatabase();

    }
}