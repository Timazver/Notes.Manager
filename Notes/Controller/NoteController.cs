using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Manager.Common.Extensions;
using Notes.Manager.Common.Network;
using Notes.Manager.Notes.Dto;
using Notes.Manager.Notes.service;

namespace Notes.Manager.Notes;

[ApiController]
[Authorize(policy: "AdminOrUser")]
[Route("api/{controller}")]
public class NotesController(NoteService noteService) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<NoteResponseDto>>> GetNotes()
    {
        var notes = await noteService.GetNotes(User.GetUserId());
        return ApiResponse.Success(notes.ConvertAll(n => n.ToResponseDto()));
    }

    [HttpGet("{noteId}")]
    public async Task<ApiResponse<NoteResponseDto>> GetNote(long noteId)
    {
        var note = await noteService.GetNote(User.GetUserId(), noteId);
        return ApiResponse.Success(note.ToResponseDto());
    }
    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteDto body)
    {
        await noteService.AddNote(body.Title, body.Content, User.GetUserId());
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpPatch("{noteId}")]
    public async Task<IActionResult> UpdateNote(long noteId, [FromBody] UpdateNoteDto body)
    {
        await noteService.UpdateNote(noteId, body.Title, body.Content, User.GetUserId());
        return StatusCode(StatusCodes.Status204NoContent);
    }
    [HttpDelete("{noteId}")]
    public async Task<IActionResult> DeleteNote(long noteId)
    {
        await noteService.DeleteNote(User.GetUserId(), noteId);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}

