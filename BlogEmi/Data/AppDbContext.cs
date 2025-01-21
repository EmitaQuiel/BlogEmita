using BlogEmi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogEmi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        


    }
}
