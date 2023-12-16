using StraffRecords.Domain.Entities;
using System.Linq.Expressions;

namespace StaffRecords.Repository.Contracts
{
    public interface IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        public TEntity Create(TEntity entity);

        public void Delete(TEntity entity);

        public TEntity Update(TEntity entity);

        public Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);

        public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        public IQueryable<TEntity> GetAll();
    }
}
