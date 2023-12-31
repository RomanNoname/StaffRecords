﻿using AutoMapper;
using MediatR;
using StaffRecords.Repository.Contracts.IRepositories;
using StaffRecords.Domain.Requests.Companies;
using StaffRecords.Domain.Responces.Companies;

namespace StaffRecords.Handlers.CompaniesHandlers
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesRequest, IEnumerable<GetCompanyResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<GetCompanyResponse>> Handle(GetAllCompaniesRequest request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<GetCompanyResponse>>(result);
        }
    }
}
