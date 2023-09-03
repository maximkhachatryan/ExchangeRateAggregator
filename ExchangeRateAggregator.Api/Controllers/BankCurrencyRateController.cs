using Microsoft.AspNetCore.Mvc;
using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices;

namespace ExchangeRateAggregator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankCurrencyRateController : ControllerBase
    {
        private readonly IBankCurrencyRateService _bankCurrencyRateService;

        public BankCurrencyRateController(IBankCurrencyRateService bankCurrencyRateService)
        {
            _bankCurrencyRateService = bankCurrencyRateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankCurrencyRateService.GetCurrencyRatesOfAllBanks();

            return Ok(result);
        }

    }
}
