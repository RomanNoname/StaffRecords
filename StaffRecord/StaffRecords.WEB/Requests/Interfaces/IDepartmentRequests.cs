using StaffRecords.WEB.DTO.Department;

namespace StaffRecords.WEB.Requests.Interfaces
{
    public interface IDepartmentRequests
    {
        public Task<IEnumerable<DeparmentDTO>> GetAllDepartmentsAsync();
    }
}
