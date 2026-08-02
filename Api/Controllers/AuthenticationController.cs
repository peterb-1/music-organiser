using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Utils;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(UserManager<ApplicationUser> userManager, TokenService tokenService) : ControllerBase
{
    private UserManager<ApplicationUser> UserManager { get; } = userManager;
    private TokenService TokenService { get; } = tokenService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await UserManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                detail: "Email is already registered. Please login."
            );
        }

        var newUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var creationResult = await UserManager.CreateAsync(newUser, request.Password);
        var errorMessage = creationResult.Errors.FlattenErrors(error => error.Description);

        if (!creationResult.Succeeded)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: errorMessage
            );
        }

        return Ok(new TokenResponse { Token = TokenService.GenerateToken(newUser.Id) });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var existingUser = await UserManager.FindByEmailAsync(request.Email);

        if (existingUser == null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: "Email is not associated with an account. Please register first."
            );
        }

        if (!await UserManager.CheckPasswordAsync(existingUser, request.Password))
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                detail: "Incorrect password. Please try again."
            );
        }

        return Ok(new TokenResponse { Token = TokenService.GenerateToken(existingUser.Id) });
    }
}
