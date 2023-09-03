using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindAsync(params object[] ids);

        Task<IEnumerable<TEntity>> GetAsync(
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> CreateAsync(TEntity entity);

        void Remove(params TEntity[] entities);
    }
}
