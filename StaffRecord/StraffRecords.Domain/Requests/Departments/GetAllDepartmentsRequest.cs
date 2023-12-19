using MediatR;
using StaffRecords.Domain.Responces.Departments;

namespace StaffRecords.Domain.Requests.Departments
{
    public record class GetAllDepartmentsRequest() : IRequest<IEnumerable<GetDepartmentResponse>>;

}
