namespace dotnet_api_first.Services.BackgroundJobs
{
    public interface IBackground
    {
        void RestoreDatabase();
        void BackUpDatabase();

    }
}