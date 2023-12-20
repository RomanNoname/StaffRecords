using Dapper;
using Microsoft.Data.SqlClient;
using StaffRecords.DataInitialisation;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Extensions;
using StaffRecords.Domain.QueryModels;
using StaffRecords.Repository.Contracts.IRepositories;
using static Dapper.SqlMapper;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }


        public async Task<IEnumerable<Employee>> GetEmployeesBySearchAsync(EmployeeQueryString queryString, CancellationToken cancellationToken)
        {
            var tableName = GetTableName<Employee>();
            var companyTableName = GetTableName<Company>();
            var departmentTableName = GetTableName<Department>();

            var selectFields = GetSelectFields<Employee>();
            var baseQuery = $"USE {_connectionInfo.DatabaseName} SELECT {selectFields} FROM {tableName} " +
                            $"INNER JOIN {companyTableName} ON {tableName}.{nameof(Employee.CompanyId)} = {companyTableName}.{nameof(Company.Id)} " +
                            $"INNER JOIN {departmentTableName} ON {tableName}.{nameof(Employee.DepartmentId)} = {departmentTableName}.{nameof(Department.Id)} " +
                            "WHERE 1 = 1 ";

            var parameters = new DynamicParameters();
            var sqlQuery = ApplyFilters(baseQuery, queryString, parameters);

            using var connection = new SqlConnection(_connectionInfo.ConnectionString);

            await connection.OpenAsync(cancellationToken);
            var result = await connection.QueryAsync<Employee>(sqlQuery, parameters);

            return result;

        }

        public async Task<decimal> GetTotalSalaryAsync(EmployeeQueryString queryString, CancellationToken cancellationToken)
        {
            var tableName = GetTableName<Employee>();
            var companyTableName = GetTableName<Company>();
            var departmentTableName = GetTableName<Department>();

            var baseQuery = $"USE {_connectionInfo.DatabaseName} SELECT SUM(Salary) FROM {tableName} " +
                            $"INNER JOIN {companyTableName} ON {tableName}.{nameof(Employee.CompanyId)} = {companyTableName}.{nameof(Company.Id)} " +
                            $"INNER JOIN {departmentTableName} ON {tableName}.{nameof(Employee.DepartmentId)} = {departmentTableName}.{nameof(Department.Id)} " +
                            "WHERE 1 = 1 ";

            var parameters = new DynamicParameters();
            var sqlQuery = ApplyFilters(baseQuery, queryString, parameters);

            using var connection = new SqlConnection(_connectionInfo.ConnectionString);

            await connection.OpenAsync(cancellationToken);
            var result = await connection.ExecuteScalarAsync<decimal>(sqlQuery, parameters);

            return result;

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

            using var connection = new SqlConnection(_connectionInfo.ConnectionString);

            await connection.OpenAsync(cancellationToken);

            await connection.ExecuteAsync(updateQuery, entity);

        }
        private string GetTableName<T>() => typeof(T).Name;
        private string GetSelectFields<T>() =>
    string.Join(", ", typeof(T).GetProperties()
                              .Where(p => p.PropertyType.IsSupportedType())
                              .Select(p => $"{GetTableName<T>()}.{p.Name}"));
        private string ApplyFilters(string baseQuery, EmployeeQueryString queryString, DynamicParameters parameters)
        {
            if (!string.IsNullOrEmpty(queryString.LastName))
            {
                baseQuery += $" AND {GetTableName<Employee>()}.{nameof(Employee.LastName)} LIKE @LastName ";
                parameters.Add("@LastName", $"%{queryString.LastName}%");
            }

            if (!string.IsNullOrEmpty(queryString.CompanyName))
            {
                baseQuery += $" AND {GetTableName<Company>()}.{nameof(Company.CompanyName)} LIKE @CompanyName ";
                parameters.Add("@CompanyName", $"%{queryString.CompanyName}%");
            }

            if (!string.IsNullOrEmpty(queryString.DepartmentName))
            {
                baseQuery += $" AND {GetTableName<Department>()}.{nameof(Department.DepartmentName)} LIKE @DepartmentName ";
                parameters.Add("@DepartmentName", $"%{queryString.DepartmentName}%");
            }

            if (queryString.SalaryFrom.HasValue)
            {
                baseQuery += $" AND {GetTableName<Employee>()}.{nameof(Employee.Salary)} >= @SalaryFrom ";
                parameters.Add("@SalaryFrom", queryString.SalaryFrom.Value);
            }

            if (queryString.SalaryTo.HasValue)
            {
                baseQuery += $" AND {GetTableName<Employee>()}.{nameof(Employee.Salary)} <= @SalaryTo ";
                parameters.Add("@SalaryTo", queryString.SalaryTo.Value);
            }

            if (queryString.DateHireFrom.HasValue)
            {
                baseQuery += $" AND {GetTableName<Employee>()}.{nameof(Employee.HireDate)} >= @DateHireFrom ";
                parameters.Add("@DateHireFrom", queryString.DateHireFrom.Value);
            }

            if (queryString.DateHireTo.HasValue)
            {
                baseQuery += $" AND {GetTableName<Employee>()}.{nameof(Employee.HireDate)} <= @DateHireTo ";
                parameters.Add("@DateHireTo", queryString.DateHireTo.Value);
            }

            return baseQuery;
        }


    }
}
