using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Manager.Auth.Domain;
using Notes.Manager.Infra;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Admin.Bootstrap;

public class AdminBootstrapService(ApplicationDbContext dbContext, IPasswordHasher<AuthCredentials> passwordHasher)

{
    public async Task<AdminBootstrapResult> CreatedAdmin(AdminBootstrapCommand command)
    {
        var existed = await dbContext.Users.FirstOrDefaultAsync(e => e.Email.Equals(command.Email));
        if (existed?.Role == Role.Admin) throw new AdminAlreadyExistingException(command.Email);
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var admin = new UserEntity(
                0,
                command.FirstName,
                command.LastName,
                true,
                command.Email,
                Role.Admin
            );
            dbContext.Users.Add(admin);
            await dbContext.SaveChangesAsync();
            var credentials = new AuthCredentials(
                0,
                admin.Id,
                admin,
                "",
                DateTimeOffset.UtcNow
            );
            var passwordHash = passwordHasher.HashPassword(credentials, command.Password);
            credentials.PasswordHash = passwordHash;
            dbContext.AuthCredentials.Add(credentials);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return AdminBootstrapResult.Created;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}