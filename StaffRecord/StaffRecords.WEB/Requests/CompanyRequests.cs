using Newtonsoft.Json;
using StaffRecords.WEB.DTO.Company;
using StaffRecords.WEB.Requests.Interfaces;

namespace StaffRecords.WEB.Requests
{
    public class CompanyRequests : ICompanyRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string SendEndpoint = "api/Company";

        public CompanyRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<CompanyDTO>>(content)!;
        }
    }
}
