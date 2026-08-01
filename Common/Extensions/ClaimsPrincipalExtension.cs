using System.Security.Claims;
using Notes.Manager.Common.Exceptions;

namespace Notes.Manager.Common.Extensions;

public static class ClaimsPrincipalExtension
{
    public static long GetUserId(this ClaimsPrincipal claimsPrincipal) =>
        long.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            ? userId : throw new InvalidUserIdClaimException();
}