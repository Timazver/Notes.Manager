namespace Notes.Manager.IntegrationTests;

public sealed record ApiResponse<T>(int Status, T? Data = default, string? Error = null);

public sealed record AccessTokenResponse(string AccessToken);

public sealed record NoteResponse(
    long Id,
    string Title,
    string Content,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt
);
