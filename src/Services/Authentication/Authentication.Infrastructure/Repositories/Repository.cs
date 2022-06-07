using Membership.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Membership.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return entities;
        }
        public Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteRangeAsync(TEntity entity)
        {
            Context.Set<TEntity>().RemoveRange(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
