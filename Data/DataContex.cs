using dotnet_api_first.models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api_first.Data
{
    public class DataContex : DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options)
        {

        }
        public DbSet<Character> characters { get; set; }
        public DbSet<User> users { get; set; }

    }
}