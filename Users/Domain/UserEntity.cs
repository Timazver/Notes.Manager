namespace Notes.Manager.Users.Domain;

public class UserEntity
{
    public UserEntity(long id, string firstName, string lastName, bool isActive, string email, Role role)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        IsActive = isActive;
        Email = email;
        Role = role;
    }

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; } = Role.User;
}