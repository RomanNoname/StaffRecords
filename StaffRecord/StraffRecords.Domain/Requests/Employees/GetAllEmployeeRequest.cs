using MediatR;
using StraffRecords.Domain.Responces.Employees;

namespace StraffRecords.Domain.Requests.Employees
{
    public class GetAllEmployeeRequest : IRequest<GetAllEmployeeResponse>
    {
    }
}
