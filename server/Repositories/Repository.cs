using Bookify.Data.Pagination;
using Microsoft.EntityFrameworkCore;
using static Bookify.Repositories.IRepository;
using System.Linq.Expressions;
using Bookify.Entities;

namespace Bookify.Repositories
{
    /// <summary>
    /// Generic repository implementation with paging support
    /// </summary>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<PagedResult<TEntity>> GetAllAsync(
            PageRequest pageRequest = null,
            CancellationToken cancellationToken = default)
        {
            pageRequest ??= PageRequest.Of(0, 25);

            var query = _dbSet.AsQueryable();

            // Get total count before applying pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply sorting
            query = ApplySorting(query, pageRequest.Sort);

            // Apply pagination
            var items = await query
                .Skip(pageRequest.Page * pageRequest.Size)
                .Take(pageRequest.Size)
                .ToListAsync(cancellationToken);

            // Create and return paged result
            return new PagedResult<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageRequest.Page,
                PageSize = pageRequest.Size
            };
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result >= 0 ? entity : null;
        }

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result >= 0 ? entity : null;
        }

        public virtual async Task<bool> DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result >= 0 ? true : false;
            }
            return false;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<PagedResult<TEntity>> FindPagedAsync(
            Expression<Func<TEntity, bool>> predicate,
            PageRequest pageRequest = null,
            CancellationToken cancellationToken = default)
        {
            pageRequest ??= PageRequest.Of(0, 25);

            var query = _dbSet.Where(predicate);

            // Get total count before applying pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply sorting
            query = ApplySorting(query, pageRequest.Sort);

            // Apply pagination
            var items = await query
                .Skip(pageRequest.Page * pageRequest.Size)
                .Take(pageRequest.Size)
                .ToListAsync(cancellationToken);

            // Create and return paged result
            return new PagedResult<TEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageRequest.Page,
                PageSize = pageRequest.Size
            };
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                return await _dbSet.CountAsync(cancellationToken);

            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Applies sorting to an IQueryable based on the provided Sort parameters
        /// </summary>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, Sort sort)
        {
            if (sort == null || string.IsNullOrWhiteSpace(sort.Column))
                return query;

            // We need to use dynamic sorting since we don't know the property at compile time
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, sort.Column);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = sort.Direction == SortDirection.ASC ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(TEntity), property.Type },
                query.Expression,
                Expression.Quote(lambda));

            return query.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
