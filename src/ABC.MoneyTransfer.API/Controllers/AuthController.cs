using ABC.MoneyTransfer.Application.DTOs;
using ABC.MoneyTransfer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABC.MoneyTransfer.API.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase {
  // [x] POST /Auth/Register : Register a new user.
  // [x] POST /Auth/Login : Log in a user.
  // [x] POST /Auth/Logout : Log out a user.

  private readonly IUserService _userService;
  public AuthenticationController(IUserService userService) {
    _userService = userService;
  }

  [HttpPost("Register")]
  public async Task<IActionResult>
  RegisterUser(ApplicationUserRegisterDTO model) {
    // var result = await _authenticationService.LoginUserAsync(model);
    // if (result)
    return Ok();
    // else
    return Unauthorized();
  }

  [HttpPost("Login")]
  public async Task<IActionResult> LoginUser(ApplicationUserLoginDTO model) {
    // var result = await _authenticationService.LoginUserAsync(model);
    // if (result)
    return Ok();
    // else
    return Unauthorized();
  }

  [HttpPost("Logout")]
  public async Task<IActionResult> LogoutUser() {
    // var result = await _authenticationService.LogoutUserAsync();
    // if (result)
    return Ok();
    // else
    return Unauthorized();
  }
}