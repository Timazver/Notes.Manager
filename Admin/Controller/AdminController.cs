using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Manager.Admin.Service;
using Notes.Manager.Common.Network;
using Notes.Manager.Notes.Dto;
using Notes.Manager.Users.Dto;

namespace Notes.Manager.Admin.Controller;

[ApiController]
[Authorize(policy: "AdminOnly")]
[Route("api")]
public class AdminController(AdminService adminService)
{
    [HttpGet("users")]
    public async Task<ApiResponse<List<UserResponseDto>>> GetAllUsers()
    {
        var users = await adminService.GetAllUsers();
        return ApiResponse.Success(users.ConvertAll(u => u.ToDto()));
    }

    [HttpGet("admin/notes")]
    public async Task<ApiResponse<List<NoteResponseDto>>> GetAllNotes()
    {
        var notes = await adminService.GetAllNotes();
        return ApiResponse.Success(notes.ConvertAll(n => n.ToResponseDto()));
    }
}
