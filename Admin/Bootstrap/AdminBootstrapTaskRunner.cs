namespace Notes.Manager.Admin.Bootstrap;

public sealed class AdminBootstrapTaskRunner(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration
) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        try
        {
            var bootstrapService =
                scope.ServiceProvider.GetRequiredService<AdminBootstrapService>();
            await bootstrapService.CreatedAdmin(ReadBootstrapCommand());
        }
        catch (AdminAlreadyExistingException)
        {
            //TOOD: логирование
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private AdminBootstrapCommand ReadBootstrapCommand()
    {
        var firstName = GetRequired("ADMIN_FIRST_NAME");
        var lastName = GetRequired("ADMIN_LAST_NAME");
        var email = GetRequired("ADMIN_EMAIL");
        var password = GetRequired("ADMIN_PASSWORD");
        var command = new AdminBootstrapCommand(
            firstName, lastName, email, password
        );
        return command;
    }

    private string GetRequired(string name)
    {
        return configuration[name]
               ?? throw new InvalidOperationException(
                   $"Environment variable '{name}' is not configured.");
    }
}