using AutoMapper;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Responces.Companies;

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
