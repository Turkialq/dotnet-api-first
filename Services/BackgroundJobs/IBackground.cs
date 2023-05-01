using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api_first.Services.BackgroundJobs
{
    public interface IBackground
    {
        void SyncData();
        void UpdateDatabase();

    }
}