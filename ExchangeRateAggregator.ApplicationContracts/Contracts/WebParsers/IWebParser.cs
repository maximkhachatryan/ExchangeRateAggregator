using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationContracts.Contracts.WebParsers
{
    public interface IWebParser
    {
        Task<Dictionary<string, ParseResult>> Parse();
    }
}
