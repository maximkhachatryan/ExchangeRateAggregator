using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationContracts.Dtos
{
    public class BankCurrencyRateDto
    {
        public int BankId { get; set; }
        public string BankName { get; set; }

        public Dictionary<string, ParseResult> CurrencyRates { get; set; }
    }
}
