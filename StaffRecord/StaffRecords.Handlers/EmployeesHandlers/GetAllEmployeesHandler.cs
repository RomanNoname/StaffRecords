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
        public Task<IEnumerable<GetEmployeeResponse>> Handle(GetAllEmployeesRequest request, CancellationToken cancellationToken)
        {
            var result = _employeeRepository.GetAll();

            return Task.FromResult(_mapper.Map<IEnumerable<GetEmployeeResponse>>(result));
        }
    }
}
