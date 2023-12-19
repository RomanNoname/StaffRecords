using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StaffRecords.Domain.Requests.Employees;
using StaffRecords.Domain.Responces.Employees;

namespace StaffRecords.Handlers.EmployeesHandlers
{
    public class GetEmployeesBySearchHandler : IRequestHandler<GetEmployeesBySearchRequest, IEnumerable<GetEmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetEmployeesBySearchHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetEmployeeResponse>> Handle(GetEmployeesBySearchRequest request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetEmployeesBySearchAsync(request.QueryString, cancellationToken);

            return _mapper.Map<IEnumerable<GetEmployeeResponse>>(result);
        }
    }
}
