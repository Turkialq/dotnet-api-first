using System.Diagnostics;

namespace dotnet_api_first.Services.BackgroundJobs
{
    public class Background : IBackground
    {
        public void RestoreDatabase()
        {
            Console.WriteLine("Restoring database");
        }

        public void BackUpDatabase()
        {
            // Get the path to the shell script
            string scriptPath = "/Users/turkialqahtani/Desktop/WebDev/dotnet-api-first/Services/BackgroundJobs/DataBaseBackup/PostgressBackup.sh";

            // Create a ProcessStartInfo object
            ProcessStartInfo psi = new ProcessStartInfo(scriptPath);

            // Start the process
            Process process = Process.Start(psi);

            // Wait for the process to finish
            process.WaitForExit();

            // Check the exit code
            if (process.ExitCode != 0)
            {
                // The script failed
                Console.WriteLine("The script failed with exit code {0}", process.ExitCode);
            }
            else
            {
                // The script succeeded
                Console.WriteLine("The script succeeded");
            }
        }
    }
}