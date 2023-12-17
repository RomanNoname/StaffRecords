using StraffRecords.Domain.Entities;
using System.Linq.Expressions;

namespace StaffRecords.Repository.Contracts
{
    public interface IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        public IQueryable<TEntity> GetAll();

    }
}
