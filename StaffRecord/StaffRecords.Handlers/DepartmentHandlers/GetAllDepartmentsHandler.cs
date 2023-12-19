using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Departments;
using StraffRecords.Domain.Responces.Departments;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Handlers.DepartmentHandlers
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsRequest, IEnumerable<GetDepartmentResponse>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public GetAllDepartmentsHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetDepartmentResponse>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _departmentRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<GetDepartmentResponse>>(result);
        }
    }
}
