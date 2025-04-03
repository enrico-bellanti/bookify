using Bookify.Data.Pagination;
using System.Linq.Expressions;

namespace Bookify.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Generic repository interface with paging support - similar to Spring Boot's JpaRepository
        /// </summary>
        public interface IRepository<TEntity, TKey> where TEntity : class
        {
            // Basic CRUD operations
            Task<PagedResult<TEntity>> GetAllAsync(
                PageRequest pageRequest = null,
                Expression<Func<TEntity, bool>> filter = null,
                CancellationToken cancellationToken = default,
                params string[] includeProperties);

            Task<TEntity> GetByIdAsync(
                TKey id,
                CancellationToken cancellationToken = default,
                params string[] includeProperties);

            Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

            Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

            Task<bool> DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);

            Task<IEnumerable<TEntity>> FindAsync(
                Expression<Func<TEntity, bool>> predicate,
                CancellationToken cancellationToken = default,
                params string[] includeProperties);

            Task<PagedResult<TEntity>> FindPagedAsync(
                Expression<Func<TEntity, bool>> predicate,
                PageRequest pageRequest = null,
                CancellationToken cancellationToken = default,
                params string[] includeProperties);

            Task<TEntity> SingleOrDefaultAsync(
                Expression<Func<TEntity, bool>> predicate,
                CancellationToken cancellationToken = default,
                params string[] includeProperties);

            Task<bool> AnyAsync(
                Expression<Func<TEntity, bool>> predicate,
                CancellationToken cancellationToken = default);

            Task<int> CountAsync(
                Expression<Func<TEntity, bool>> predicate = null,
                CancellationToken cancellationToken = default);
        }
    }
}
