using ExchangeRateAggregator.ApplicationContracts.Contracts.Services.ApplicationServices;
using ExchangeRateAggregator.ApplicationContracts.Dtos;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using ExchangeRateAggregator.ApplicationContracts.Exceptions;
using ExchangeRateAggregator.ApplicationServices.WebParsers;
using ExchangeRateAggregator.ApplicationServices.WebParsers.ParsersByScrapping;
using ExchangeRateAggregator.Domain.Contracts;
using ExchangeRateAggregator.Domain.Entities;
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
        private readonly WebParserFactory _parserFactory;

        public BankCurrencyRateService(
            IUnitOfWork unitOfWork,
            WebParserFactory parserFactory)
        {
            this._unitOfWork = unitOfWork;
            this._parserFactory = parserFactory;
        }

        public async Task<IEnumerable<BankCurrencyRateDto>> GetCurrencyRatesOfAllBanks()
        {
            var currencyRates = await _unitOfWork.CurrencyRatesRepository.GetAsync(
                null,
                x => x.Bank);

            var result = currencyRates.GroupBy(c => c.BankId).Select(g => new BankCurrencyRateDto
            {
                BankId = g.Key,
                BankName = g.First().Bank.Name,
                CurrencyRates = new Dictionary<string, ParseResult>(g.Select(
                    x => new KeyValuePair<string, ParseResult>(
                        x.CurrencyCode, new ParseResult
                        {
                            BuyRate = x.BuyRate,
                            SellRate = x.SellRate
                        })))
            });

            return result;
        }

        public async Task ParseBankCurrencyRates(int bankId)
        {
            var bank = await _unitOfWork.BanksRepository.GetSingleOrDefaultAsync(b => b.Id == bankId);
            if (bank == null)
                throw new NotFoundException("Provided BankId is missing");

            var parser = _parserFactory.CreateParser(bank);
            var parseResult = await parser.Parse();

            await UpdateCurrencyRates(bank.Id, parseResult);

        }

        public async Task UpdateBankCurrencyRates(int bankId, Dictionary<string, ParseResult> currencyRates)
        {
            var bank = await _unitOfWork.BanksRepository.GetSingleOrDefaultAsync(b => b.Id == bankId);
            if (bank == null)
                throw new NotFoundException("Provided BankId is missing");

            await UpdateCurrencyRates(bank.Id, currencyRates);
        }

        private async Task UpdateCurrencyRates(int bankId, Dictionary<string, ParseResult> currencyRates)
        {
            var bankCurrencyRates = await _unitOfWork.CurrencyRatesRepository.GetAsync(
                c => c.Bank.Id == bankId);

            foreach (var item in currencyRates)
            {
                var currencyCode = item.Key;
                var existingCurrencyRate = bankCurrencyRates.FirstOrDefault(
                    c => c.CurrencyCode == currencyCode);

                if (existingCurrencyRate == null)
                {
                    await _unitOfWork.CurrencyRatesRepository.CreateAsync(new CurrencyRate
                    {
                        CurrencyCode = currencyCode,
                        BuyRate = item.Value.BuyRate,
                        SellRate = item.Value.SellRate,
                        BankId = bankId
                    });
                }
                else
                {
                    existingCurrencyRate.BuyRate = item.Value.BuyRate;
                    existingCurrencyRate.SellRate = item.Value.SellRate;

                    _unitOfWork.CurrencyRatesRepository.UpdatePartially(
                        existingCurrencyRate,
                        nameof(CurrencyRate.BuyRate),
                        nameof(CurrencyRate.SellRate));
                }
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
