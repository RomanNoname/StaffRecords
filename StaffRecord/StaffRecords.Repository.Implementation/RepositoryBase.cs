using Humanizer;
using Microsoft.EntityFrameworkCore;
using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts;
using StraffRecords.Domain.Entities;
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
            var propertyNames = entityType.GetProperties()
                                          .Where(p =>
                                                 p.PropertyType.IsPrimitive ||
                                                 p.PropertyType == typeof(string) ||
                                                 p.PropertyType == typeof(Guid) ||
                                                 p.PropertyType == typeof(DateTime) ||
                                                 p.PropertyType == typeof(int?) ||
                                                 p.PropertyType == typeof(double?) ||
                                                 p.PropertyType == typeof(decimal) ||
                                                 p.PropertyType == typeof(float?) ||
                                                 p.PropertyType == typeof(long?) ||
                                                 p.PropertyType == typeof(short?) ||
                                                 p.PropertyType == typeof(byte?) ||
                                                 p.PropertyType == typeof(Guid?) ||
                                                 p.PropertyType == typeof(DateTime?)
                                           )
                                          .Select(p => p.Name);

            var selectFields = string.Join(", ", propertyNames);
            var sqlQuery = $"SELECT {selectFields} FROM {tableName}";

            var result = Context.Set<TEntity>().FromSqlInterpolated(FormattableStringFactory.Create(sqlQuery));

            return result.AsQueryable();
        }


    }
}
