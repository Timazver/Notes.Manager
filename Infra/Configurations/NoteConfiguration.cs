using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Manager.Notes.Domain;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Infra.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.ToTable("notes");
        builder.HasKey("Id");
        builder.Property(n => n.Title)
            .HasMaxLength(30)
            .IsRequired();
        builder.Property(n => n.Content)
            .IsRequired();
        builder.Property(n => n.CreatedAt)
            .IsRequired();
        builder.Property(n => n.UpdatedAt)
            .IsRequired();
        builder.Property(n => n.IsArchived)
            .HasDefaultValue(false)
            .IsRequired();
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}