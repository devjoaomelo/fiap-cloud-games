using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Context
{
    public class FCGDbContext : DbContext
    {
        public FCGDbContext(DbContextOptions<FCGDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FCGDbContext).Assembly);
        }
    }
}
