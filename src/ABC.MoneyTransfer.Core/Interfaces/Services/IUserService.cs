namespace ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.DTOs;

public interface IUserService {
  Task<OperationResult<ApplicationUserRegistrationResponseDTO>>
  RegisterAsync(ApplicationUserRegistrationRequestDTO registerDto);
  Task<OperationResult<ApplicationUserRegistrationResponseDTO>>
  LoginAsync(ApplicationUserLoginDTO loginDto);
}