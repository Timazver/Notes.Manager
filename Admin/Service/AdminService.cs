using Microsoft.EntityFrameworkCore;
using Notes.Manager.Infra;
using Notes.Manager.Notes.Domain;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Admin.Service;

public class AdminService(ApplicationDbContext dbContext)
{
    public async Task<List<UserEntity>> GetAllUsers()
    {
        var users = await dbContext.Users.ToListAsync();
        return users;
    }

    public async Task<List<NoteEntity>> GetAllNotes()
    {
        return await dbContext.Notes.ToListAsync();
    }
}
