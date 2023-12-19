using Newtonsoft.Json;
using StaffRecords.WEB.DTO.Department;
using StaffRecords.WEB.Requests.Interfaces;

namespace StaffRecords.WEB.Requests
{
    public class DepartmentRequests : IDepartmentRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string SendEndpoint = "api/Department";
        public DepartmentRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }

        public async Task<IEnumerable<DeparmentDTO>> GetAllDepartmentsAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<DeparmentDTO>>(content)!;
        }
    }
}
