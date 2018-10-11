using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackCodeChallenge.Data
{
    public class UpstackDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public UpstackDbContext(DbContextOptions<UpstackDbContext> options) : base(options)
        {

        }
        public UpstackDbContext()
        {
        }
    }
}
