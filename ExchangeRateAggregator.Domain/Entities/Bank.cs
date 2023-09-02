using ExchangeRateAggregator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Domain.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public WebParser Parser { get; set; }

        public virtual ICollection<CurrencyRate> CurrencyRates { get; set; }
    }
}
