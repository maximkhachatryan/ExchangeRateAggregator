using ExchangeRateAggregator.ApplicationContracts.Contracts.WebParsers;
using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers
{
    internal abstract class ParserBase : IWebParser
    {
        protected readonly string _source;
        protected readonly IHttpClientFactory _httpClientFactory;

        public ParserBase(string source, IHttpClientFactory httpClientFactory)
        {
            this._source = source;
            this._httpClientFactory = httpClientFactory;
        }

        public abstract Task<Dictionary<string, ParseResult>> Parse();
    }
}
