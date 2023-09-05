using ExchangeRateAggregator.Domain.Enums;

namespace ExchangeRateAggregator.ApplicationContracts.Attributes.WebParsers
{
    public class WebParserAttribute : Attribute
    {
        public WebParserAttribute(WebParser parser)
        {
            Parser = parser;
        }

        public WebParser Parser { get; }
    }
}