using MediatR;
using StraffRecords.Domain.Responces.Employees;

namespace StraffRecords.Domain.Requests.Employees
{
    public record class GetAllEmployeeRequest() : IRequest<IEnumerable<GetEmployeeResponse>>;
}
