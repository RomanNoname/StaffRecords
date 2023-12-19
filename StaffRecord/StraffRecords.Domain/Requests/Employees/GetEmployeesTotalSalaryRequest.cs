using MediatR;
using StaffRecords.Domain.QueryModels;

namespace StaffRecords.Domain.Requests.Employees
{
    public record class GetEmployeesTotalSalaryRequest(EmployeeQueryString EmployeeQueryString):IRequest<decimal>;
}
