using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Manager.Common.Extensions;
using Notes.Manager.Common.Network;
using Notes.Manager.Users.Dto;
using Notes.Manager.Users.Service;

namespace Notes.Manager.Users.Controller;

[ApiController]
[Authorize(policy: "AdminOrUser")]
[Route("api")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ApiResponse<UserResponseDto>> GetUserInfo()
    {
        var userInfo = await userService.GetUserInfo(User.GetUserId());

        return ApiResponse.Success(userInfo.ToDto());
    }
}