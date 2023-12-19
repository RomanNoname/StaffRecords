using MediatR;
using StaffRecords.Domain.QueryModels;
using StaffRecords.Domain.Responces.Employees;

namespace StaffRecords.Domain.Requests.Employees
{
    public record class GetEmployeesBySearchRequest(EmployeeQueryString QueryString) : IRequest<IEnumerable<GetEmployeeResponse>>;
}
