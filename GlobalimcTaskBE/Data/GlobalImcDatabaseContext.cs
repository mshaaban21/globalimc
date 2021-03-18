using GlobalimcTaskBE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalimcTaskBE.Data
{
    public class GlobalImcDatabaseContext : DbContext
    {
        public GlobalImcDatabaseContext(DbContextOptions<GlobalImcDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
