using StaffRecords.Admin.DTO.Employee;
using StraffRecords.Domain.SearchString;

namespace StaffRecords.Admin.Requests.Interfaces
{
    public interface IEmployeeRequests
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesBySearchAsync(EmployeeQueryString employeeQueryString);
    }
}
