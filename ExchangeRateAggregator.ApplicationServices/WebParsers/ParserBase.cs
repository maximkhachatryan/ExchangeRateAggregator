using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.ApplicationServices.WebParsers
{
    internal class ParserBase
    {
        private readonly string _source;

        public ParserBase(string source)
        {
            this._source = source;
        }
    }
}
