using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api_first.DTOs.user
{
    public class UserLoginDTO
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

    }
}