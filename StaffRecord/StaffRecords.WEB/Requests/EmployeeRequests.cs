using Newtonsoft.Json;
using StaffRecords.Frontend.Shared;
using StaffRecords.Frontend.Shared.Requests;
using StaffRecords.WEB.DTO.Employee;
using StaffRecords.WEB.Requests.Interfaces;
using StraffRecords.Domain.SearchString;

namespace StaffRecords.WEB.Requests
{
    public class EmployeeRequests : IEmployeeRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string _sendEnpoint = "api/Employee";
        public EmployeeRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{_sendEnpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(content)!;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesBySearchAsync(EmployeeQueryString employeeQueryString)
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{_sendEnpoint}/search?{employeeQueryString.ToQueryStringWithDate()}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(content)!;
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO)
        {
            await _httpApiRequests.SendPutAsyncRequest($"{_sendEnpoint}", updateEmployeeDTO);
        }

        public async Task<decimal> GetEmployeesTotalSalaryAsync(EmployeeQueryString employeeQueryString)
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{_sendEnpoint}/totalSalary?{employeeQueryString.ToQueryStringWithDate()}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<decimal>(content)!;
        }
    }

}
