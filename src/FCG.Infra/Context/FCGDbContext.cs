using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Context;

public class FCGDbContext : DbContext
{
    public FCGDbContext(DbContextOptions<FCGDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<UserGame> UserGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FCGDbContext).Assembly);
    }
}

