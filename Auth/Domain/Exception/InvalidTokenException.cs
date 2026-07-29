namespace Notes.Manager.Auth.Domain.Exception;

public class InvalidTokenException(string message = "Неверный токен!") : System.Exception(message);