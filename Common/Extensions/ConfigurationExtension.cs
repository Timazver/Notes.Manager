namespace Notes.Manager.Common.Extensions;

public static class ConfigurationExtension
{
    public static string GetRequiredEnv(this IConfiguration configuration, string name)
    {
        var value = configuration[name];

        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException(
                $"Environment variable '{name}' is not configured or empty.");

        return value;
    }
}