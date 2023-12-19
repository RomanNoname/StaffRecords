using AutoMapper;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Responces.Companies;

namespace StaffRecords.Host.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, GetCompanyResponse>();
        }
    }
}
