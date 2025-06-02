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

        builder.Property(ug => ug.Id)
            .IsRequired();

        builder.Property(ug => ug.PurchaseDate)
            .IsRequired();

        builder.HasOne(ug => ug.User)
            .WithMany(u => u.UserGames)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ug => ug.Game)
            .WithMany(g => g.UserGames)
            .HasForeignKey(ug => ug.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ug => new { ug.UserId, ug.GameId }).IsUnique();
    }
}