using Notes.Manager.Common;

namespace Notes.Manager.Notes.Domain.exceptions;

public class NoteNotFoundException(
    string message = "Записи не найдена!"
) : NotFoundException(message);