using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Manager.Auth.Domain;
using Notes.Manager.Auth.Domain.Exception;
using Notes.Manager.Infra;
using Notes.Manager.Users.Domain;
using Notes.Manager.Users.Domain.Exception;

namespace Notes.Manager.Auth.Service;

public class AuthService(
    JwtTokenService tokenService,
    ApplicationDbContext dbContext,
    IPasswordHasher<AuthCredentials> passwordHasher
)
{
    public async Task Register(RegisterCommand registerCommand)
    {
        var existedUser = await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Email == registerCommand.Email);
        if (existedUser != null) throw new EmailAlreadyException();
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new UserEntity(
                0,
                registerCommand.FirstName, registerCommand.LastName, true, registerCommand.Email, Role.User
            );
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var userCredentials = new AuthCredentials(
                0,
                user.Id,
                user,
                "",
                DateTimeOffset.UtcNow
            );
            userCredentials.PasswordHash = passwordHasher.HashPassword(userCredentials, registerCommand.Password);
            dbContext.AuthCredentials.Add(userCredentials);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<string> Login(string email, string password)
    {
        var userCredentials = await CheckExistingAuthCredentials(email);
        if (userCredentials.User == null) throw new UserNotFoundException();
        var result = passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.PasswordHash, password);
        return result == PasswordVerificationResult.Success
            ? tokenService.GenerateToken(userId: userCredentials.UserId, userCredentials.User.Role)
            : throw new InvalidCredentialException();
    }


    private async Task<AuthCredentials> CheckExistingAuthCredentials(string email)
    {
        var userCredentials = await dbContext
            .AuthCredentials
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.User.Email == email);

        return userCredentials ?? throw new InvalidCredentialException();
    }
}