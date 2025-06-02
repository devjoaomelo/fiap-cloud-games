using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Configurations;

public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder.ToTable("UserGames");

        builder.HasKey(ug => ug.Id);

        builder.Property(ug => ug.UserId)
            .IsRequired();

        builder.Property(ug => ug.GameId)
            .IsRequired();
        
        builder.HasIndex(ug => new { ug.UserId, ug.GameId })
            .IsUnique();
    }
}