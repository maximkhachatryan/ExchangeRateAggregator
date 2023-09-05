using ExchangeRateAggregator.ApplicationContracts.Contracts.WebParsers;
using ExchangeRateAggregator.ApplicationServices.WebParsers.ParsersByScrapping;
using ExchangeRateAggregator.Domain.Entities;
using ExchangeRateAggregator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers
{
    public class WebParserFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebParserFactory(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public IWebParser CreateParser(Bank bank)
        {
            //TODO: Replace switch operator with reflection based on the Concrete Parser Attributes
            switch (bank.Parser)
            {
                case WebParser.AmeriaBank:
                    return new AmeriaBankParser(bank.Source, _httpClientFactory);



                default:
                    throw new NotImplementedException("WebParser not found");
                
            }
        }
    }
}
