using Dapper;
using Microsoft.Data.SqlClient;
using StaffRecords.DatainItialisation;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Extensions;
using StraffRecords.Domain.SearchString;
using static Dapper.SqlMapper;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesBySearchAsync(EmployeeQueryString queryString)
        {

            var entityType = typeof(Employee);

            var tableName = entityType.Name;
            var companyTableName = typeof(Company).Name;
            var departmentTableName = typeof(Department).Name;

            var propertyNames = entityType.GetProperties()
                .Where(p => p.PropertyType.IsSupportedType())
                .Select(p => $"{tableName}.{p.Name}");

            var selectFields = string.Join(", ", propertyNames);
            var sqlQuery = $"Use {_connectionInfo.DatabaseName} SELECT {selectFields} FROM {tableName} " +
                           $"INNER JOIN {companyTableName} ON {tableName}.{nameof(Employee.CompanyId)} = {companyTableName}.{nameof(Company.Id)} " +
                           $"INNER JOIN {departmentTableName} ON {tableName}.{nameof(Employee.DepartmentId)} = {departmentTableName}.{nameof(Department.Id)} " +
                           $"WHERE 1 = 1 ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(queryString.LastName))
            {
                sqlQuery += $"AND {tableName}.{nameof(Employee.LastName)} LIKE @LastName ";
                parameters.Add("@LastName", $"%{queryString.LastName}%");
            }

            if (!string.IsNullOrEmpty(queryString.CompanyName))
            {
                sqlQuery += $"AND {companyTableName}.{nameof(Company.CompanyName)} LIKE @CompanyName ";
                parameters.Add("@CompanyName", $"%{queryString.CompanyName}%");
            }

            if (!string.IsNullOrEmpty(queryString.DepartmentName))
            {
                sqlQuery += $"AND {departmentTableName}.{nameof(Department.DepartmentName)} LIKE @DepartmentName ";
                parameters.Add("@DepartmentName", $"%{queryString.DepartmentName}%");
            }

            if (queryString.SalaryFrom.HasValue)
            {
                sqlQuery += $"AND {tableName}.{nameof(Employee.Salary)} >= @SalaryFrom ";
                parameters.Add("@SalaryFrom", queryString.SalaryFrom.Value);
            }

            if (queryString.SalaryTo.HasValue)
            {
                sqlQuery += $"AND {tableName}.{nameof(Employee.Salary)} <= @SalaryTo ";
                parameters.Add("@SalaryTo", queryString.SalaryTo.Value);
            }

            using (var connection = new SqlConnection(_connectionInfo.ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Employee>(sqlQuery, parameters);
                return result;
            }
        }

        public async Task UpdateAsync(Employee entity, CancellationToken cancellationToken)
        {
            var entityType = typeof(Employee);
            var tableName = entityType.Name;

            var propertyNames = entityType.GetProperties()
                                           .Where(p => p.PropertyType.IsSupportedType())
                                           .Select(p => p.Name);

            var updateFields = string.Join(", ", propertyNames.Select(p => $"{p} = @{p}"));
            var updateQuery = $"Use {_connectionInfo.DatabaseName} UPDATE {tableName} SET {updateFields} WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionInfo.ConnectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(updateQuery, entity);
            }
        }


    }
}
