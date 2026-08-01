namespace Notes.Manager.Infra;

public static class DatabaseConnectionStringBuilder
{
    public static string Build(IConfiguration configuration)
    {

        var host = configuration["DB_HOST"]
                   ?? throw new InvalidOperationException("DB_HOST is missing");

        var port = configuration["DB_PORT"]
                   ?? "5432";

        var database = configuration["DB_NAME"]
                       ?? throw new InvalidOperationException("DB_NAME is missing");

        var username = configuration["DB_USER"]
                       ?? throw new InvalidOperationException("DB_USER is missing");

        var password = configuration["DB_PASS"]
                       ?? throw new InvalidOperationException("DB_PASS is missing");

        return new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = int.Parse(port),
            Database = database,
            Username = username,
            Password = password
        }.ConnectionString;
    }
}