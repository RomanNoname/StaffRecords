﻿using StraffRecords.Domain.Entities;
using StraffRecords.Domain.SearchString;

namespace StaffRecords.Repository.Contracts.IRepositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
      public IQueryable<Employee> GetEmployeesBySearch(EmployeeQueryString queryString);

      public Task UpdateAsync(Employee entity, CancellationToken cancellationToken);
    }
}
