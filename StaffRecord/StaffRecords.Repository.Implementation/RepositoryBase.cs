using Humanizer;
using Microsoft.EntityFrameworkCore;
using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Extensions;
using System.Runtime.CompilerServices;

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


        public IQueryable<TEntity> GetAll()
        {
            var entityType = typeof(TEntity);

            var tableName = entityType.Name.Pluralize();
           
            var sqlQuery = $"SELECT * FROM {tableName}";

            var result = Context.Set<TEntity>().FromSqlInterpolated(FormattableStringFactory.Create(sqlQuery));

            return result.AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entityType = typeof(TEntity);
            var tableName = entityType.Name.Pluralize();
           
            var selectQuery = $"SELECT * FROM {tableName} WHERE Id = '{id}'";

            return await Context.Set<TEntity>().FromSqlRaw(selectQuery).FirstOrDefaultAsync(cancellationToken);
        }


    }
}
