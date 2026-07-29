namespace Notes.Manager.Auth.Service;

public sealed record RegisterCommand(string FirstName, string LastName, string Email, string Password);