using Microsoft.EntityFrameworkCore;
using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts;
using StraffRecords.Domain.Entities;
using System.Linq.Expressions;

namespace StaffRecords.Repository.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
     where TEntity : BaseEntity
    {
        protected RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        protected ApplicationDbContext Context { get; }

        public TEntity Create(TEntity entity)
        {
            Context.Add(entity);

            return entity;
        }

        public void Delete(TEntity entity) => Context.Remove(entity);

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);

            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token) =>
            await Context.Set<TEntity>().SingleOrDefaultAsync(entity => entity.Id == id, token);

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression) =>
            Context.Set<TEntity>().Where(expression).AsNoTracking();

        public IQueryable<TEntity> GetAll() => Context.Set<TEntity>().AsNoTracking();

        public async Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
        {
            await Context.AddRangeAsync(entities, cancellationToken);
            return entities;
        }
    }
}
