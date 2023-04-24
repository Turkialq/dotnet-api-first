using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;
using dotnet_api_first.DTOs;

namespace dotnet_api_first.Data
{
    public interface IAuthRepo
    {
        Task<ServiceRespinse<int>> Register(User user, string password);
        Task<ServiceRespinse<string>> Login(string userName, string password);
        Task<bool> UserExists(string userName);

    }
}