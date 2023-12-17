using MediatR;
using StraffRecords.Domain.Responces.Employees;
using StraffRecords.Domain.SearchString;

namespace StraffRecords.Domain.Requests.Employees
{
    public record class GetEmployeesBySearchRequest(EmployeeQueryString QueryString) : IRequest<IEnumerable<GetEmployeeResponse>>;
}
