using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.DTOs;
using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ABC.MoneyTransfer.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<TransactionResponseDTO>>
    CreateTransactionAsync(TransactionRequestDTO request)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Amount = request.Amount,
            Description = request.Description,
            Timestamp = DateTime.UtcNow
        };

        var createdTransaction =
            await _repository.CreateTransactionAsync(transaction);
        if (createdTransaction is null)
        {
            return new OperationResult<TransactionResponseDTO>()
            {
                Success = false,
                Message = "Transaction creation failed."
            };
        }

        return new OperationResult<TransactionResponseDTO>()
        {
            Success = true,
            Message = "Transaction created successfully",
            Data =
              new TransactionResponseDTO
              {
                  Id = createdTransaction.Id,
                  UserId = createdTransaction.UserId,
                  Amount = createdTransaction.Amount,
                  Description = createdTransaction.Description,
                  Timestamp = createdTransaction.Timestamp
              }
        };
    }

    public async Task<OperationResult<TransactionResponseDTO>>
    GetTransactionByIdAsync(Guid id)
    {
        var transaction = await _repository.GetTransactionByIdAsync(id);
        if (transaction == null)
        {
            return new OperationResult<TransactionResponseDTO>()
            {
                Success = false,
                Message = "Transaction not found."
            };
        }

        return new OperationResult<TransactionResponseDTO>()
        {
            Success = true,
            Data = new TransactionResponseDTO
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Timestamp = transaction.Timestamp
            }
        };
    }

    public async Task<IEnumerable<TransactionResponseDTO>>
    GetUserTransactionsAsync(Guid userId)
    {
        var transactions = await _repository.GetUserTransactionsAsync(userId);
        return transactions
            .Select(t => new TransactionResponseDTO
            {
                Id = t.Id,
                UserId = t.UserId,
                Amount = t.Amount,
                Description = t.Description,
                Timestamp = t.Timestamp
            })
            .ToList();
    }
}