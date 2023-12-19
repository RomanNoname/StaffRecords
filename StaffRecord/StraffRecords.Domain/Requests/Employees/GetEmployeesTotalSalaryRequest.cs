using MediatR;
using StraffRecords.Domain.SearchString;

namespace StraffRecords.Domain.Requests.Employees
{
    public record class GetEmployeesTotalSalaryRequest(EmployeeQueryString EmployeeQueryString):IRequest<decimal>;
}
