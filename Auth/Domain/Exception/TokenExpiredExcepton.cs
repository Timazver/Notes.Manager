namespace Notes.Manager.Auth.Domain.Exception;

public class TokenExpiredException(string mesage = "Токен не содержит обязательный claim: email.")
    : System.Exception(mesage);