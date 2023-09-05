using ExchangeRateAggregator.ApplicationContracts.Dtos.WebParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers.ParsersByApi
{
    internal abstract class ParserByApi : ParserBase
    {
        public ParserByApi(string source, IHttpClientFactory httpClientFactory)
            : base(source, httpClientFactory)
        {
        }
    }
}
