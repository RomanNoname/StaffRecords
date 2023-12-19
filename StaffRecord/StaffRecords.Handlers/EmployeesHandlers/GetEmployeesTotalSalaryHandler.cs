using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class GetEmployeesTotalSalaryHandler : IRequestHandler<GetEmployeesTotalSalaryRequest, decimal>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesTotalSalaryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<decimal> Handle(GetEmployeesTotalSalaryRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetTotalSalaryAsync(request.EmployeeQueryString, cancellationToken);

            return result;
        }
    }
}
