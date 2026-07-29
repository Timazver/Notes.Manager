namespace Notes.Manager.Admin.Bootstrap;

public class AdminAlreadyExistingException(string Email)
    : Exception($"Cannot bootstrap administrator: email {Email} already belongs to another user");