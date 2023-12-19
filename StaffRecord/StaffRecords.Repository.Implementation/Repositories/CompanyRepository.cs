using StaffRecords.DataInitialisation;
using StaffRecords.Repository.Contracts.IRepositories;
using StaffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(ConnectionInfo connectionInfo):base(connectionInfo) { }
   
    }
}
