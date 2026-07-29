using System.ComponentModel.DataAnnotations;

namespace Notes.Manager.Common.Validation;

public sealed class NotBlankAttribute : RequiredAttribute
{
    public NotBlankAttribute()
    {
        ErrorMessage = "Поле не должно быть пустым или состоять только из пробелов";
    }
}