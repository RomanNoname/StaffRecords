﻿using StaffRecords.Domain.Entities;
using StaffRecords.Domain.QueryModels;

namespace StaffRecords.Repository.Contracts.IRepositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
      public Task<IEnumerable<Employee>> GetEmployeesBySearchAsync(EmployeeQueryString queryString, CancellationToken cancellationToken);

      public Task UpdateAsync(Employee entity, CancellationToken cancellationToken);

       public Task<decimal> GetTotalSalaryAsync(EmployeeQueryString queryString, CancellationToken cancellationToken);
    }
}
