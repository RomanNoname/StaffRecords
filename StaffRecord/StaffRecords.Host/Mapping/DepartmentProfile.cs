using AutoMapper;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Responces.Departments;

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
