﻿using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

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
        public Task<IEnumerable<GetEmployeeResponse>> Handle(GetEmployeesBySearchRequest request, CancellationToken cancellationToken)
        {
            var result = _employeeRepository.GetEmployeesBySearch(request.QueryString);

            return Task.FromResult(_mapper.Map<IEnumerable<GetEmployeeResponse>>(result));
        }
    }
}