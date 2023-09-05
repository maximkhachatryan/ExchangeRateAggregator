using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers.ParsersByScrapping
{
    internal abstract class ParserByScrapping : ParserBase
    {
        public ParserByScrapping(string source, IHttpClientFactory httpClientFactory)
            : base(source, httpClientFactory)
        {
        }
    }
}
