using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, GetAllEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<GetAllEmployeeResponse> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetAllEmployeeResponse(_employeeRepository.GetAll().ToList()));
        }
    }
}
