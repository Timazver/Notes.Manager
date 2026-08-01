using Notes.Manager.Users.Domain;

namespace Notes.Manager.Users.Dto;

public record UserResponseDto(
    long Id,
    string FirstName,
    string LastName,
    bool IsActive,
    string Email,
    string Role);

public static class UserEntityExtension
{
    public static UserResponseDto ToDto(this UserEntity entity)
    {
        return new UserResponseDto(
            entity.Id, entity.FirstName, entity.LastName, entity.IsActive, entity.Email, entity.Role.GetAuthority());
    }
}