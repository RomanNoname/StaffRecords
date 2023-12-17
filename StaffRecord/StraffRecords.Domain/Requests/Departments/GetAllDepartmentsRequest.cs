using MediatR;
using StraffRecords.Domain.Responces.Departments;

namespace StraffRecords.Domain.Requests.Departments
{
    public record class GetAllDepartmentsRequest() : IRequest<IEnumerable<GetDepartmentResponse>>;

}
