namespace Notes.Manager.Common.Exceptions;

public class InvalidUserIdClaimException(string message = "Authenticated user does not contain a valid user identifier.") : Exception(message);