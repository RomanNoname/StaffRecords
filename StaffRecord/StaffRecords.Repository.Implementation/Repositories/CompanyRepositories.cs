
using StaffRecords.DataAcess;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Entities;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class CompanyRepositories : RepositoryBase<Company>, ICompanyRepository
    {
        protected CompanyRepositories(ApplicationDbContext context) : base(context)
        {
        }
    }
}
