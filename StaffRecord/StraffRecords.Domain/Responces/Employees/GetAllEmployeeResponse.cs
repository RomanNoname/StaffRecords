using StraffRecords.Domain.Entities;

namespace StraffRecords.Domain.Responces.Employees
{
    public record class GetAllEmployeeResponse(IEnumerable<Employee> Employees);
}
