using Business.Abstract;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        var result = await _authService.RegisterAsync(registerDTO);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var result = await _authService.LoginAsync(loginDTO);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("refreshLogin")]
    public async Task<IActionResult> RefreshLogin(string refreshToken)
    {
        var result = await _authService.RefreshLoginAsync(refreshToken);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var result = await _authService.LogoutAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> AssignRole(AssignRoleDTO entity)
    {
        var result = await _authService.AssignRoleAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> RemoveRole(RemoveRoleDTO entity)
    {
        var result = await _authService.RemoveRoleAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }
}
