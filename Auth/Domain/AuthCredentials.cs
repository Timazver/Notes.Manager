using Notes.Manager.Users.Domain;

namespace Notes.Manager.Auth.Domain;

public class AuthCredentials
{
    private AuthCredentials()
    {
    }

    public AuthCredentials(long id, long userId, UserEntity user, string passwordHash, DateTimeOffset createdAt)
    {
        Id = id;
        UserId = userId;
        User = user;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public long Id { get; set; }
    public long UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public string PasswordHash { get; set; } = "";
    public DateTimeOffset CreatedAt { get; set; }
}