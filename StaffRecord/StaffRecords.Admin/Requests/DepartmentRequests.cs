using Newtonsoft.Json;
using StaffRecords.Admin.DTO.Department;
using StaffRecords.Admin.Requests.Interfaces;
using StaffRecords.Frontend.Shared.Requests;

namespace StaffRecords.Admin.Requests
{
    public class DepartmentRequests : IDepartmentRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string _sendEnpoint = "api/Department";
        public DepartmentRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }
        public async Task<IEnumerable<DeparmentDTO>> GetAllDepartmentsAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{_sendEnpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<DeparmentDTO>>(content)!;
        }
    }
}
