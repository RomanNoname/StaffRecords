using StaffRecords.Admin.DTO.Department;

namespace StaffRecords.Admin.Requests.Interfaces
{
    public interface IDepartmentRequests
    {
        public Task<IEnumerable<DeparmentDTO>> GetAllDepartmentsAsync();
    }
}
