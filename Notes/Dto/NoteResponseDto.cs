using Notes.Manager.Notes.Domain;

namespace Notes.Manager.Notes.Dto;

public record NoteResponseDto(
    long Id,
    string Title,
    string Content,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);

public static class NoteEntityExt
{
    public static NoteResponseDto ToResponseDto(this NoteEntity entity)
    {
        return new NoteResponseDto
        (
            entity.Id,
            entity.Title,
            entity.Content,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}