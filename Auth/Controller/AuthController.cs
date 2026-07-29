using Microsoft.AspNetCore.Mvc;
using Notes.Manager.Auth.Dto;
using Notes.Manager.Auth.Service;
using Notes.Manager.Common;

namespace Notes.Manager.Auth.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ApiResponse<AuthResponseDto>> Login([FromBody] AuthRequestDto dto)
    {
        var token = await authService.Login(dto.Email, dto.Password);
        return ApiResponse.Success(new AuthResponseDto(token));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        await authService.Register(dto.ToRegisterCommand());
        return StatusCode(StatusCodes.Status201Created);
    }
}