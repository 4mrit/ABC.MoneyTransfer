using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.DTOs;
using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ABC.MoneyTransfer.Application.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(IUserRepository repository,
                       IPasswordHasher<ApplicationUser> passwordHasher,
                       SignInManager<ApplicationUser> signInManager)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _signInManager = signInManager;
    }

    public async Task<OperationResult<ApplicationUserRegistrationResponseDTO>>
    RegisterAsync(ApplicationUserRegistrationRequestDTO userDto)
    {
        var existingUser = await _repository.GetUserByEmailAsync(userDto.Email);
        if (existingUser != null)
        {
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = false,
                Message = "User Already Exists !!",
            };
        }

        var user = new ApplicationUser
        {
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            MiddleName = userDto.MiddleName,
            LastName = userDto.LastName,
            Address = userDto.Address,
        };

        var passwordHash = _passwordHasher.HashPassword(user, userDto.Password);
        var createdUser = await _repository.CreateUserAsync(user, passwordHash);
        if (createdUser is null)
        {
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = false,
                Message = "User Registred Unsuccessful.",
            };
        }
        return new OperationResult<ApplicationUserRegistrationResponseDTO>()
        {
            Success = true,
            Message = "User Registred Successfully",
            Data =
              new ApplicationUserRegistrationResponseDTO()
              {
                  Id = createdUser.Id,
                  Email = createdUser.Email!,
                  FirstName = createdUser.FirstName,
                  LastName = createdUser.LastName,
                  Country = createdUser.Country,
                  MiddleName = createdUser.MiddleName,
                  Address = createdUser.Address
              }
        };
    }

    public async Task<OperationResult<ApplicationUserRegistrationResponseDTO>>
    LoginAsync(ApplicationUserLoginDTO loginDto)
    {
        var user = await _repository.GetUserByEmailAsync(loginDto.Email);
        if (user != null)
        {
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = false,
                Message = "User Already Exists !!",
            };
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(
            user, loginDto.Password, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            await _signInManager.PasswordSignInAsync(user, loginDto.Password,
                                                     isPersistent: false,
                                                     lockoutOnFailure: false);
            Console.WriteLine("Login Successful");
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = true,
                Message = "Successfully Signed In !!",
            };
        }

        if (signInResult.IsLockedOut)
        {
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = false,
                Message = "User Locked Out!!",
            };
        }
        else if (signInResult.IsNotAllowed)
        {
            return new OperationResult<ApplicationUserRegistrationResponseDTO>()
            {
                Success = false,
                Message = "User Not Allowed to Sign In!!",
            };
        }
        Console.WriteLine("Login Unsuccessful");
        return new OperationResult<ApplicationUserRegistrationResponseDTO>()
        {
            Success = false,
            Message = "User Logged In Unsuccessful!!",
        };
    }
}