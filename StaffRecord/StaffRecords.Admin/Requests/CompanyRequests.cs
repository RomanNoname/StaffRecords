using Newtonsoft.Json;
using StaffRecords.Admin.DTO.Company;
using StaffRecords.Admin.Requests.Interfaces;
using StaffRecords.Frontend.Shared.Requests;

namespace StaffRecords.Admin.Requests
{
    public class CompanyRequests : ICompanyRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string _sendEnpoint = "api/Company";
        public CompanyRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }
        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{_sendEnpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<CompanyDTO>>(content)!;
        }
    }
}
