using MediatR;
using StaffRecords.Domain.Responces.Employees;

namespace StaffRecords.Domain.Requests.Employees
{
    public record class GetAllEmployeesRequest() : IRequest<IEnumerable<GetEmployeeResponse>>;
}
