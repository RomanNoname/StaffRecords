using StraffRecords.Domain.Entities;

namespace StaffRecords.Repository.Contracts
{
    public interface IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        public Task<TEntity?> GetByIdAsync(Guid id,CancellationToken cancellationToken);

    }
}
