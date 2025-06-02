using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");
        
        builder.HasKey(g => g.Id);
        
        builder.OwnsOne(g => g.Title, title =>
        {
            title.Property(t => t.Name)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(150);
        });
        
        builder.OwnsOne(g => g.Description, description =>
        {
            description.Property(d => d.Text)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(1000);
        });

        builder.OwnsOne(g => g.Price, price =>
        {
            price.Property(p => p.Value)
                .HasColumnName("Price")
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        });

        builder
            .HasMany(g => g.UserGames)
            .WithOne(ug => ug.Game)
            .HasForeignKey(ug => ug.GameId);
    }
}