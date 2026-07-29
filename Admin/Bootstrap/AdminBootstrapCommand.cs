namespace Notes.Manager.Admin.Bootstrap;

public enum AdminBootstrapResult
{
    Created,
    AlreadyExists
}

public record AdminBootstrapCommand(string FirstName, string LastName, string Email, string Password);