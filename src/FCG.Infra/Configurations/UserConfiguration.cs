using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infra.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.Profile)
               .IsRequired()
               .HasConversion<string>();

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(254);
            email.HasIndex(e => e.Address).IsUnique();
        });

        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(p => p.Hash)
                    .HasColumnName("PasswordHash")
                    .IsRequired()
                    .HasMaxLength(60);
        });
    }
}

