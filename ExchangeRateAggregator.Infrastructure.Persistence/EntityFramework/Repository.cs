using ExchangeRateAggregator.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Infrastructure.Persistence.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ExchangeRateAggregatorDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ExchangeRateAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity?> FindAsync(params object[] ids)
            => await _dbSet.FindAsync(ids);

        public async Task<IEnumerable<TEntity>> GetAsync(
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            SkipTake(ref query, skip, take);
            return await query.AsSplitQuery().ToListAsync();
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        private void SkipTake(
            ref IQueryable<TEntity> query,
            int? skip = null,
            int? take = null)
        {
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
                if (take.HasValue)
                    query = query.Take(take.Value);
            }
        }

        private IQueryable<TEntity> Include(
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _dbSet.AsNoTracking();

            return includeProperties.Aggregate(
                query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
