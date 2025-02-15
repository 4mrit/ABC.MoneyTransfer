namespace ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.DTOs;

public interface ITransactionService
{
    Task<OperationResult<TransactionResponseDTO>>
    CreateTransactionAsync(TransactionRequestDTO request);
    Task<OperationResult<TransactionResponseDTO>>
    GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<TransactionResponseDTO>>
    GetUserTransactionsAsync(Guid userId);
}