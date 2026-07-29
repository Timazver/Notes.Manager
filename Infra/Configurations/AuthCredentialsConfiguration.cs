using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Manager.Auth.Domain;

namespace Notes.Manager.Infra.Configurations;

public class AuthCredentialsConfiguration : IEntityTypeConfiguration<AuthCredentials>
{
    public void Configure(EntityTypeBuilder<AuthCredentials> builder)
    {
        builder.ToTable("auth_credentials");
        builder.HasKey(a => a.Id);
        builder.HasOne(credentials => credentials.User)
            .WithOne()
            .HasForeignKey<AuthCredentials>(credentials => credentials.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.PasswordHash).IsRequired();
    }
}