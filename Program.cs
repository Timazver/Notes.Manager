using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Notes.Manager.Admin.Bootstrap;
using Notes.Manager.Auth;
using Notes.Manager.Auth.Config;
using Notes.Manager.Common;
using Notes.Manager.Infra;
using Npgsql;

Env
    .NoClobber()
    .TraversePath()
    .Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(BuildConnectionString(builder.Configuration)))
    .AddAdminBootstrapFeature(builder.Configuration)
    .AddSingleton(BuildJwtOptionsFromEnv(builder.Configuration))
    .AddAuthFeature()
    .AddControllers();

var app = builder.Build();
// app.UseExceptionHandler();
app.MapControllers();
app.UseAuthorization();

app.Run();

static JwtOptions BuildJwtOptionsFromEnv(IConfiguration configuration)
{
    return new JwtOptions(
        configuration.GetRequiredEnv("JWT_ISSUER"),
        configuration.GetRequiredEnv("JWT_AUDIENCE"),
        configuration.GetRequiredEnv("JWT_SECRET"),
        int.Parse(configuration.GetRequiredEnv("JWT_EXPIRATION"))
    );
}

static string BuildConnectionString(IConfiguration configuration)
{
    var host = configuration.GetRequiredEnv("DB_HOST");
    var port = int.Parse(configuration.GetRequiredEnv("DB_PORT"));
    var database = configuration.GetRequiredEnv("DB_NAME");
    var username = configuration.GetRequiredEnv("DB_USER");
    var password = configuration.GetRequiredEnv("DB_PASS");

    return new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = port,
        Database = database,
        Username = username,
        Password = password
    }.ConnectionString;
}