using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        protected EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
