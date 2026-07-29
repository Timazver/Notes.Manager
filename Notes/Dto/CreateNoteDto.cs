using System.ComponentModel.DataAnnotations;
using Notes.Manager.Common.Validation;

namespace Notes.Manager.Notes.Dto;

public sealed record CreateNoteDto(
    [NotBlank][MaxLength(30)] string Title,
    [Required] string Content
);