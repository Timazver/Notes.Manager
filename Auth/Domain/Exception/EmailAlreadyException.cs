namespace Notes.Manager.Auth.Domain.Exception;

public class EmailAlreadyException(
    string message = "Пользователь с таким email уже существует"
) : System.Exception(message);