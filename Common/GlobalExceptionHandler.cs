using Microsoft.AspNetCore.Diagnostics;

namespace Notes.Manager.Common;

public class GlobalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}