using StaffRecords.WEB.DTO.Employee;
using StaffRecords.Domain.QueryModels;

namespace StaffRecords.WEB.Requests.Interfaces
{
    public interface IEmployeeRequests
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesBySearchAsync(EmployeeQueryString employeeQueryString);
        public Task UpdateEmployeeAsync(UpdateEmployeeDTO updateEmployeeDTO);

        public Task<decimal> GetEmployeesTotalSalaryAsync(EmployeeQueryString employeeQueryString);
    }
}
