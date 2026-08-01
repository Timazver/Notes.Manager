using Microsoft.EntityFrameworkCore;
using Notes.Manager.Infra;
using Notes.Manager.Users.Domain;
using Notes.Manager.Users.Domain.Exception;

namespace Notes.Manager.Users.Service;

public class UserService(ApplicationDbContext dbContext)
{
    public async Task<UserEntity> GetUserInfo(long userId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user ?? throw new UserNotFoundException();
    }
}