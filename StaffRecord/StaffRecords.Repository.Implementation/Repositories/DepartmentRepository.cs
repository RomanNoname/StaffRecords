using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
