using Newtonsoft.Json;
using StaffRecords.Admin.DTO.Employee;
using StaffRecords.Admin.Requests.Interfaces;
using StaffRecords.Frontend.Shared;
using StaffRecords.Frontend.Shared.Requests;
using StraffRecords.Domain.SearchString;

namespace StaffRecords.Admin.Requests
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


    }

}
