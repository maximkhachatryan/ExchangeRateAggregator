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
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void UpdatePartially(TEntity entity, params string[] properties)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            var entry = _dbContext.Entry(entity);
            if (properties.Any())
            {
                foreach (var prop in properties)
                    entry.Property(prop).IsModified = true;
            }
            else
            {
                entry.State = EntityState.Modified;
            }
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        public void Remove(params TEntity[] entities)
        {
            foreach (var item in entities)
            {
                _dbSet.Remove(item);
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
