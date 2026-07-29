using System.ComponentModel.DataAnnotations;
using Notes.Manager.Common.Validation;

namespace Notes.Manager.Auth.Dto;

public sealed record AuthRequestDto(
    [NotBlank]
    [MaxLength(30)]
    [EmailAddress]
    string Email,
    [Required] string Password
);