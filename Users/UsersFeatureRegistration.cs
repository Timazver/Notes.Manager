using Notes.Manager.Users.Service;

namespace Notes.Manager.Users;

public static class UsersFeatureRegistration
{
    public static IServiceCollection AddUsersFeature(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        return services;
    }
}
