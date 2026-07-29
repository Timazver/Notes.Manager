using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Infra.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName)
            .IsRequired();
        builder.Property(u => u.LastName)
            .IsRequired();
        builder.Property(u => u.Email)
            .IsRequired();
        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();
    }
}