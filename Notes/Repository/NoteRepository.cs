// using Notes.Manager.Infra;
// using Notes.Manager.Notes.Domain;
//
// namespace Notes.Manager.Notes.Repository;
//
// public class NoteRepository(ApplicationDbContext db)
// {
//     private findUserOrThrow(email: String) : UserEntity =
//     private UserNotFoundException()
//
//     private List<NoteEntity> GetNotes(string email)
//     {
//     }
//
//     private fun getNotes(email: String):List<NoteEntity> {
//         val user = findUserOrThrow(email)
//         return repo.findNoteEntitiesByUserId(user.id!!)
//     }
//
//     private fun getNote(
//         id:Long,
//     email:string,
//     ):NoteEntity {
//         val user = findUserOrThrow(email)
//         val note = repo.getNoteEntityByIdAndUserId(id, user.id!!)
//         return note ?: throw NoteNotFoundException()
//     }
//
//     userRepo.findByEmail(email)
//         ?: throw
// }

