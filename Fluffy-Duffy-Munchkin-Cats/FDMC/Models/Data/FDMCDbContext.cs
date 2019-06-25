using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Models.Data
{
    public class FDMCDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public FDMCDbContext(DbContextOptions<FDMCDbContext> options)
         : base(options)
        {

        }

        public FDMCDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-KDM5RU8\\SQLEXPRESS;Database=FDMC;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
