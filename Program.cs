using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Notes.Manager.Admin;
using Notes.Manager.Auth;
using Notes.Manager.Auth.Config;
using Notes.Manager.Common;
using Notes.Manager.Common.Extensions;
using Notes.Manager.Infra;
using Notes.Manager.Notes;
using Notes.Manager.Users;

Env
    .NoClobber()
    .TraversePath()
    .Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(DatabaseConnectionStringBuilder.Build(builder.Configuration)))
    .AddAdminFeature(builder.Configuration)
    .AddSingleton(BuildJwtOptionsFromEnv(builder.Configuration))
    .AddAuthFeature()
    .AddNoteFeature()
    .AddUsersFeature()
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
