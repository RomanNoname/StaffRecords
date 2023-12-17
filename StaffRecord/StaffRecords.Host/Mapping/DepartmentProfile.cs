using AutoMapper;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Responces.Departments;

namespace StaffRecords.Host.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, GetDepartmentResponse>();
        }

    }
}
