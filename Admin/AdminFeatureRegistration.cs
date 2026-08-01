using Notes.Manager.Admin.Bootstrap;
using Notes.Manager.Admin.Service;

namespace Notes.Manager.Admin;

public static class AdminFeatureRegistration
{
    public static IServiceCollection AddAdminFeature(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AdminService>();
        services.AddAdminBootstrapFeature(configuration);
        return services;
    }
}
