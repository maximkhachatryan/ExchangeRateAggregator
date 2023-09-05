using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Domain.Entities
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
