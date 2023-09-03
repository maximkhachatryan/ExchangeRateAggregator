using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers
{
    public record ParseResult
    {
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
    }
}
