using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_api_first.models
{
    public class User
    {
        public int id { get; set; }
        public string userName { get; set; } = string.Empty;
        public byte[] password { get; set; } = new byte[0];
        public byte[] passwordSalt { get; set; } = new byte[0];
        public List<Character> characters { get; set; }

    }
}