namespace Notes.Manager.Notes.Domain;

public class NoteEntity
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsArchived { get; set; }

    public long UserId { get; set; }
}