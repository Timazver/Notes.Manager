using Microsoft.EntityFrameworkCore;
using Notes.Manager.Infra;
using Notes.Manager.Notes.Domain;
using Notes.Manager.Notes.Domain.exceptions;

namespace Notes.Manager.Notes.service;

public class NoteService(ApplicationDbContext dbContext)
{
    public async Task<List<NoteEntity>> GetNotes(long userId)
    {
        return await dbContext.Notes.Where(n => n.UserId == userId).ToListAsync();
    }
    public async Task<NoteEntity> GetNote(long userId, long noteId)
    {
        return await dbContext.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId)
               ?? throw new NoteNotFoundException();
    }

    public async Task AddNote(string title, string content, long userId)
    {
        var now = DateTimeOffset.UtcNow;
        var noteEntity = new NoteEntity
        {
            Id = 0,
            Title = title,
            Content = content,
            CreatedAt = now,
            UpdatedAt = now,
            IsArchived = false,
            UserId = userId,
        };
        dbContext.Add(noteEntity);
        await dbContext.SaveChangesAsync();

    }
    public async Task UpdateNote(long noteId, string? title, string? content, long userId)
    {
        int rowsUpdated = await dbContext.Notes
            .Where(n => n.Id == noteId && n.UserId == userId)
            .ExecuteUpdateAsync(s =>
            {
                var update = s.SetProperty(n => n.UpdatedAt, DateTimeOffset.UtcNow);
                if (title != null) update = update.SetProperty(n => n.Title, title);
                if (content != null) update = update.SetProperty(n => n.Content, content);
            });

        if (rowsUpdated == 0)
        {
            throw new NoteNotFoundException();
        }

    }
    public async Task DeleteNote(long noteId, long userId)
    {
        int rowsAffected = await dbContext.Notes.Where(n => n.Id == noteId && n.UserId == userId).ExecuteDeleteAsync();
        if (rowsAffected == 0) throw new NoteNotFoundException();
    }
}