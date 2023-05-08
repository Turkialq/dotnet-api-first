namespace dotnet_api_first.Services.BackgroundJobs
{
    public class Background : IBackground
    {
        public void SyncData()
        {
            Console.WriteLine("Testing HangFire: SynData");
        }

        public void UpdateDatabase()
        {
            Console.WriteLine("Testing HangFire: UpdateDatabase");
        }
    }
}