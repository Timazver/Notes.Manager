using Microsoft.AspNetCore.Identity;
using Notes.Manager.Auth.Domain;

namespace Notes.Manager.Admin.Bootstrap;

public static class AdminBootstrapRegistration
{
    public static IServiceCollection AddAdminBootstrapFeature(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<IPasswordHasher<AuthCredentials>, PasswordHasher<AuthCredentials>>();
        serviceCollection.AddScoped<AdminBootstrapService>();
        var bootstrapEnabled =
            configuration.GetValue<bool>("ADMIN_BOOTSTRAP_ENABLED");
        if (bootstrapEnabled) serviceCollection.AddHostedService<AdminBootstrapTaskRunner>();
        return serviceCollection;
    }
}