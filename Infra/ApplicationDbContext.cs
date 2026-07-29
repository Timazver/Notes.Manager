using Microsoft.EntityFrameworkCore;
using Notes.Manager.Auth.Domain;
using Notes.Manager.Notes.Domain;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Infra;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<NoteEntity> Notes { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<AuthCredentials> AuthCredentials { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}