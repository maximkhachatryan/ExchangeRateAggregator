using ExchangeRateAggregator.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Bank> BanksRepository { get; }

        public IRepository<CurrencyRate> CurrencyRatesRepository { get; }

        Task CompleteAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

    }
}
