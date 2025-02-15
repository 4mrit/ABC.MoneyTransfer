using ABC.MoneyTransfer.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ABC.MoneyTransfer.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentExchangeRate()
    {
        var result = await _exchangeRateService.GetCurrentExchangeRateAsync();
        if (result.Success)
            return Ok(result.Data);

        return BadRequest(result.Errors);
    }
}