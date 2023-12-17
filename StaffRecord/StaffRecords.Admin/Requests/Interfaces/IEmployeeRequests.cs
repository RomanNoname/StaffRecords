using StaffRecords.Admin.DTO.Employee;

namespace StaffRecords.Admin.Requests.Interfaces
{
    public interface IEmployeeRequests
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllEmployeeAsync();
    }
}
