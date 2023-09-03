using ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices;
using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using ExchangeRateAggregator.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices
{
    public class BankCurrencyRateService : IBankCurrencyRateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankCurrencyRateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BankCurrencyRateDto>> GetCurrencyRatesOfAllBanks()
        {
            var currencyRates = await _unitOfWork.CurrencyRatesRepository.GetAsync(
                null,
                null,
                x => x.Bank);

            var result = currencyRates.GroupBy(c => c.Bank).Select(g => new BankCurrencyRateDto
            {
                BankId = g.Key.Id,
                BankName = g.Key.Name,
                CurrencyRates = new Dictionary<string, ParseResult>(g.Select(
                    x => new KeyValuePair<string, ParseResult>(
                        x.CurrencyCode, new ParseResult
                        {
                            BuyRate = x.BuyRate,
                            SellRate = x.SellRate
                        }
                        )))
            });

            return result;
        }
    }
}
