using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                return;
            }

            employee.DateUpdated = DateTime.UtcNow;

            _mapper.Map(request, employee);

            await _employeeRepository.UpdateAsync(employee, cancellationToken);

        }
    }
}
