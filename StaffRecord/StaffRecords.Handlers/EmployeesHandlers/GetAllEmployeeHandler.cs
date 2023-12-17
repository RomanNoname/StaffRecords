using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, IEnumerable<GetEmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeeHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public Task<IEnumerable<GetEmployeeResponse>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
        {
            var result = _employeeRepository.GetAll().ToList();

            return Task.FromResult(_mapper.Map<IEnumerable<GetEmployeeResponse>>(result));
        }
    }
}
