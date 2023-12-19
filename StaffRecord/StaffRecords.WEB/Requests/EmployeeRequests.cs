using Newtonsoft.Json;
using StaffRecords.WEB.DTO.Employee;
using StaffRecords.WEB.Extensions;
using StaffRecords.WEB.Requests.Interfaces;
using StaffRecords.Domain.QueryModels;

namespace StaffRecords.WEB.Requests
{
    public class EmployeeRequests : IEmployeeRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string SendEndpoint = "api/Employee";

        public EmployeeRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(content)!;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesBySearchAsync(EmployeeQueryString employeeQueryString)
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/search?{employeeQueryString.ToQueryStringWithDate()}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(content)!;
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO)
        {
            await _httpApiRequests.SendPutAsyncRequest($"{SendEndpoint}", updateEmployeeDTO);
        }

        public async Task<decimal> GetEmployeesTotalSalaryAsync(EmployeeQueryString employeeQueryString)
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/totalSalary?{employeeQueryString.ToQueryStringWithDate()}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<decimal>(content)!;
        }
    }

}
