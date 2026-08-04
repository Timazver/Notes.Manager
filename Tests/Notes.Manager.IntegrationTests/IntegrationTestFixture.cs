using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Notes.Manager.Infra;
using Testcontainers.PostgreSql;

namespace Notes.Manager.IntegrationTests;

public sealed class IntegrationTestFixture : IAsyncLifetime
{
    private const string AdminEmail = "integration.admin@example.com";
    private const string AdminPassword = "Admin-password-123";

    private readonly PostgreSqlContainer database = new PostgreSqlBuilder()
        .WithImage("postgres:17-alpine")
        .WithDatabase("notes_test")
        .WithUsername("notes_test")
        .WithPassword("notes_test_password")
        .Build();

    private WebApplicationFactory<Program>? factory;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await database.StartAsync();
        ConfigureEnvironment();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(database.GetConnectionString())
            .Options;
        await using (var dbContext = new ApplicationDbContext(options))
        {
            await dbContext.Database.EnsureCreatedAsync();
        }

        factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));
        Client = factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        Client?.Dispose();
        if (factory != null) await factory.DisposeAsync();
        await database.DisposeAsync();
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var response = await Client.PostAsJsonAsync("/api/auth/login", new
        {
            email,
            password
        });
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<ApiResponse<AccessTokenResponse>>();
        return body!.Data!.AccessToken;
    }

    public async Task<string> LoginAsAdminAsync()
    {
        return await LoginAsync(AdminEmail, AdminPassword);
    }

    public static void Authorize(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    private void ConfigureEnvironment()
    {
        var connectionString = new Npgsql.NpgsqlConnectionStringBuilder(database.GetConnectionString());
        Environment.SetEnvironmentVariable("DB_HOST", connectionString.Host);
        Environment.SetEnvironmentVariable("DB_PORT", connectionString.Port.ToString());
        Environment.SetEnvironmentVariable("DB_NAME", connectionString.Database);
        Environment.SetEnvironmentVariable("DB_USER", connectionString.Username);
        Environment.SetEnvironmentVariable("DB_PASS", connectionString.Password);
        Environment.SetEnvironmentVariable("JWT_ISSUER", "notes-integration-tests");
        Environment.SetEnvironmentVariable("JWT_AUDIENCE", "notes-integration-tests-client");
        Environment.SetEnvironmentVariable(
            "JWT_SECRET",
            "integration-test-secret-key-that-is-long-enough-for-hmac-sha256"
        );
        Environment.SetEnvironmentVariable("JWT_EXPIRATION", "3600");
        Environment.SetEnvironmentVariable("ADMIN_FIRST_NAME", "Integration");
        Environment.SetEnvironmentVariable("ADMIN_LAST_NAME", "Admin");
        Environment.SetEnvironmentVariable("ADMIN_EMAIL", AdminEmail);
        Environment.SetEnvironmentVariable("ADMIN_PASSWORD", AdminPassword);
        Environment.SetEnvironmentVariable("ADMIN_BOOTSTRAP_ENABLED", "true");
    }
}
