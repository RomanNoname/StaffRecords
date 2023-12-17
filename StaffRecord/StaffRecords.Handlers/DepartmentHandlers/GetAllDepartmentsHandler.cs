using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Departments;
using StraffRecords.Domain.Responces.Departments;

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
        public Task<IEnumerable<GetDepartmentResponse>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var result = _departmentRepository.GetAll();

            return Task.FromResult(_mapper.Map<IEnumerable<GetDepartmentResponse>>(result));
        }
    }
}
