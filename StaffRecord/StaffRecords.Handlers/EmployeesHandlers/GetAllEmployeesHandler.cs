using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesRequest, IEnumerable<GetEmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeesHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetEmployeeResponse>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetAllAsync(cancellationToken);
            
            return _mapper.Map<IEnumerable<GetEmployeeResponse>>(result);
        }
    }
}
