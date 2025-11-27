using LibraryManagementAPI.Models.DTOs.Auth;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<APIResponse<string>>> Register(RegisterRequestDTO request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<APIResponse<string>>> Login(LoginRequestDTO request)
    {
        var result = await _authService.LoginAsync(request);
        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult<APIResponse<object>> Me()
    {
        string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string? name = User.Identity?.Name;
        string? role = User.FindFirstValue(ClaimTypes.Role);

        var data = new { id = id, name = name, role = role };
        return Ok(APIResponse<object>.Ok(data));
    }
}