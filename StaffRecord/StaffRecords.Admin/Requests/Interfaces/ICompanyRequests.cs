using StaffRecords.Admin.DTO.Company;

namespace StaffRecords.Admin.Requests.Interfaces
{
    public interface ICompanyRequests
    {
        public Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
    }
}
