using ABC.MoneyTransfer.Core.DTOs;
using ABC.MoneyTransfer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABC.MoneyTransfer.API.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    // [x] POST /Auth/Register : Register a new user.
    // [x] POST /Auth/Login : Log in a user.

    private readonly IUserService _userService;
    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult>
    RegisterUser(ApplicationUserRegistrationRequestDTO model)
    {
        var result = await _userService.RegisterAsync(model);
        if (result.Success)
        {
            return Ok();
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(ApplicationUserLoginDTO model)
    {
        var result = await _userService.LoginAsync(model);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        else
        {
            return Unauthorized();
        }
    }
}