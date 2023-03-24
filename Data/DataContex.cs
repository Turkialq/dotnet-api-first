using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_api_first.models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_api_first.Data
{
    public class DataContex : DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options)
        {

        }
        public DbSet<Character> characters { get; set; } // create a new table

    }
}