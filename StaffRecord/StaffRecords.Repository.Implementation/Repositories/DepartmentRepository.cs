using StaffRecords.DatainItialisation;
using StaffRecords.Repository.Contracts.IRepositories;
using StaffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ConnectionInfo connectionInfo) : base(connectionInfo) { }
    }
}
