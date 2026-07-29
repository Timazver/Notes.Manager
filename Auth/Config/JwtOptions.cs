namespace Notes.Manager.Auth.Config;

public record JwtOptions(string Issuer, string Audience, string JwtSecret, int JwtExpiration);