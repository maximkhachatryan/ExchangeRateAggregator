using ExchangeRateAggregator.Domain.Contracts;
using ExchangeRateAggregator.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Infrastructure.Persistence.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExchangeRateAggregatorDbContext _context;

        public UnitOfWork(ExchangeRateAggregatorDbContext context)
        {
            this._context = context;
        }

        private IRepository<Bank>? _banksRepository;
        public IRepository<Bank> BanksRepository
        {
            get
            {
                _banksRepository ??= new Repository<Bank>(_context);
                return _banksRepository;
            }
        }


        private IRepository<CurrencyRate>? _currencyRatesRepository;
        public IRepository<CurrencyRate> CurrencyRatesRepository
        {
            get
            {
                _currencyRatesRepository ??= new Repository<CurrencyRate>(_context);
                return _currencyRatesRepository;
            }
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
