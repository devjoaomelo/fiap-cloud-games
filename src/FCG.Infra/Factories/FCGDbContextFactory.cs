using FCG.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FCG.Infra;
public class FCGDbContextFactory : IDesignTimeDbContextFactory<FCGDbContext>
{
    public FCGDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FCGDbContext>();
        optionsBuilder.UseMySql(
            "server=localhost;database=FCGDb;user=admin;password=admin",
            new MySqlServerVersion(new Version(8, 0, 41))
        );

        return new FCGDbContext(optionsBuilder.Options);
    }
}

