using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices
{
    public interface IBankCurrencyRateService : IApplicationService
    {
        Task<IEnumerable<BankCurrencyRateDto>> GetCurrencyRatesOfAllBanks();
        Task ParseBankCurrencyRates(int bankId);
        Task UpdateBankCurrencyRates(int bankId, Dictionary<string, ParseResult> currencyRates);

    }
}
