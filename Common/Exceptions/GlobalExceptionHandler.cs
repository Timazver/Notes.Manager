using Microsoft.AspNetCore.Diagnostics;
using Notes.Manager.Admin.Bootstrap;
using Notes.Manager.Auth.Domain.Exception;
using Notes.Manager.Common.Network;
using Notes.Manager.Users.Domain.Exception;

namespace Notes.Manager.Common.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message) = exception switch
        {
            EmailAlreadyException or AdminAlreadyExistingException =>
                (StatusCodes.Status409Conflict, exception.Message),
            InvalidCredentialsException or InvalidUserIdClaimException or InvalidTokenException or TokenExpiredException =>
                (StatusCodes.Status401Unauthorized, exception.Message),
            UserNotFoundException or NotFoundException =>
                (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error.")
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(
            ApiResponse.Failure<string>(statusCode, message),
            cancellationToken: cancellationToken
        );

        return true;
    }
}
