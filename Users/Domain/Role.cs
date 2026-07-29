namespace Notes.Manager.Users.Domain;

public enum Role
{
    Admin,
    User
}

internal static class RoleExtension
{
    public static string GetAuthority(this Role role)
    {
        return role switch
        {
            Role.Admin => "ADMIN",
            Role.User => "USER",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}