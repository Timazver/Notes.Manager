using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Notes.Manager.Auth.Config;
using Notes.Manager.Auth.Service;
using Notes.Manager.Users.Domain;

namespace Notes.Manager.Auth;

public static class AuthFeatureRegistration
{
    public static IServiceCollection AddAuthFeature(this IServiceCollection services)
    {
        var jwtOptions = services.BuildServiceProvider().GetService<JwtOptions>();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.ClaimsIssuer = jwtOptions!.Issuer;
                    options.Audience = jwtOptions!.Audience;
                    options.TokenValidationParameters = BuildParams(jwtOptions!);
                }
            );
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole(Role.Admin.GetAuthority()));

            // Для обычных пользователей (или всех авторизованных)
            options.AddPolicy("UserOnly", policy =>
                policy.RequireRole(Role.User.GetAuthority()));
            options.AddPolicy("AdminOrUser", policy =>
                policy.RequireRole(Role.User.GetAuthority(), Role.Admin.GetAuthority()));
        });
        services.AddScoped<JwtTokenService>();
        services.AddScoped<AuthService>();
        return services;
    }

    private static TokenValidationParameters BuildParams(JwtOptions jwtOptions)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtSecret)),

            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
}