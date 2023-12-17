using Humanizer;
using Microsoft.EntityFrameworkCore;
using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.SearchString;
using System.Runtime.CompilerServices;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<Employee> GetEmployeesBySearch(EmployeeQueryString queryString)
        {
            var entityType = typeof(Employee);
            var tableName = entityType.Name.Pluralize();

            var companyTableName = typeof(Company).Name.Pluralize();
            var departmentTableName = typeof(Department).Name.Pluralize();

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
                                          .Select(p => $"{tableName}.{p.Name}");

            var selectFields = string.Join(", ", propertyNames);
            var sqlQuery = $"SELECT {selectFields} FROM {tableName} " +
                           $"INNER JOIN {companyTableName} ON {tableName}.{nameof(Company)}Id = {companyTableName}.Id " +
                           $"INNER JOIN {departmentTableName} ON {tableName}.{nameof(Department)}Id = {departmentTableName}.Id " +
                           $"WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(queryString.LastName))
            {
                sqlQuery += $"AND {tableName}.{nameof(queryString.LastName)} LIKE '%{queryString.LastName}%' ";
            }

            if (!string.IsNullOrEmpty(queryString.CompanyName))
            {
                sqlQuery += $"AND {companyTableName}.{nameof(queryString.CompanyName)} LIKE '%{queryString.CompanyName}%' ";
            }

            if (!string.IsNullOrEmpty(queryString.DepartmentName))
            {
                sqlQuery += $"AND {departmentTableName}.{nameof(queryString.DepartmentName)} LIKE '%{queryString.DepartmentName}%' ";
            }

            if (queryString.SalaryFrom.HasValue)
            {
                sqlQuery += $"AND {tableName}.Salary >= {queryString.SalaryFrom} ";
            }

            if (queryString.SalaryTo.HasValue)
            {
                sqlQuery += $"AND {tableName}.Salary <= {queryString.SalaryTo} ";
            }


            var result = Context.Set<Employee>().FromSqlInterpolated(FormattableStringFactory.Create(sqlQuery));

            return result.AsQueryable();
        }


    }
}
