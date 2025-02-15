using ABC.MoneyTransfer.Core.DTOs;
using ABC.MoneyTransfer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABC.MoneyTransfer.API.Controllers;

[ApiController]
[Route("Auth")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult>
    CreateTransaction(TransactionRequestDTO model)
    {
        var result = await _transactionService.CreateTransactionAsync(model);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        var result = await _transactionService.GetTransactionByIdAsync(id);
        if (result.Success)
        {
            return Ok(result.Data);
        }
        else
        {
            return NotFound(result.Message);
        }
    }

    [HttpGet("User/{userId}")]
    public async Task<IActionResult> GetUserTransactions(Guid userId)
    {
        var result = await _transactionService.GetUserTransactionsAsync(userId);
        return Ok(result);
    }
}