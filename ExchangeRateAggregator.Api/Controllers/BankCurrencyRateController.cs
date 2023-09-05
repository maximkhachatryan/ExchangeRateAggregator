using Microsoft.AspNetCore.Mvc;
using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;

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

        /// <summary>
        /// Gets all currency rates grouped by banks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankCurrencyRateService.GetCurrencyRatesOfAllBanks();

            return Ok(result);
        }

        /// <summary>
        /// Executes the WebParser to update Currency Rates from the source of spcified bank
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        [HttpPost("Parse/{bankId}")]
        public async Task<IActionResult> ParseBankCurrencyRates(int bankId)
        {
            await _bankCurrencyRateService.ParseBankCurrencyRates(bankId);

            return NoContent();
        }

        /// <summary>
        /// This method is responsible for
        ///  - inserting new currency rates for the scecified bank
        ///  - updateing rates for the existing currencies of the specified bank
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        [HttpPost("Update/{bankId}")]
        public async Task<IActionResult> UpdateBankCurrencyRates(int bankId, Dictionary<string, ParseResult> currencyRates)
        {
            await _bankCurrencyRateService.UpdateBankCurrencyRates(bankId, currencyRates);

            return Ok();
        }
    }
}
