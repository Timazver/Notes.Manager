using System.ComponentModel.DataAnnotations;
using Notes.Manager.Auth.Service;
using Notes.Manager.Common.Validation;

namespace Notes.Manager.Auth.Dto;

public sealed record RegisterRequestDto(
    [Required][NotBlank][MaxLength(30)] string FirstName,
    [Required][NotBlank][MaxLength(30)] string LastName,
    [Required][NotBlank][MaxLength(255)] string Email,
    [Required][NotBlank] string Password
)
{
    public RegisterCommand ToRegisterCommand()
    {
        return new RegisterCommand(
            FirstName,
            LastName,
            Email,
            Password
        );
    }
}