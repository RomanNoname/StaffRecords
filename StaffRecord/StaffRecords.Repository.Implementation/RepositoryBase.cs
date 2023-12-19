using Dapper;
using Microsoft.Data.SqlClient;
using StaffRecords.DataInitialisation;
using StaffRecords.Domain.Entities;
using StaffRecords.Repository.Contracts;

namespace StaffRecords.Repository.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
     where TEntity : BaseEntity
    {
        public readonly ConnectionInfo _connectionInfo;

        public RepositoryBase(ConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entityType = typeof(TEntity);
            var tableName = typeof(TEntity);

            var connectionString = _connectionInfo.ConnectionString;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync(cancellationToken);

            var sqlQuery = $"Use {_connectionInfo.DatabaseName} SELECT * FROM {tableName.Name}";

            var entities = await connection.QueryAsync<TEntity>(sqlQuery);

            return entities;

        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var tableName = typeof(TEntity).Name;

            var connectionString = _connectionInfo.ConnectionString;

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync(cancellationToken);

            var selectQuery = $"Use {_connectionInfo.DatabaseName} SELECT * FROM {tableName} WHERE Id = @Id";

            var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(selectQuery, new { Id = id });

            return entity;

        }


    }
}
