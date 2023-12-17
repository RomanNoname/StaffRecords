using StaffRecords.WEB.DTO.Company;

namespace StaffRecords.WEB.Requests.Interfaces
{
    public interface ICompanyRequests
    {
        public Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
    }
}
