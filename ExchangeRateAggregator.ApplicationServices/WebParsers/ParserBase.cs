using ExchangeRateAggregator.ApplicationContracts.Contracts.WebParsers;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers
{
    internal abstract class ParserBase : IWebParser
    {
        private readonly string _source;

        public ParserBase(string source)
        {
            this._source = source;
        }

        public abstract Dictionary<string, ParseResult> Parse(string source);
    }
}
