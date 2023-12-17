using AutoMapper;
using StaffRecords.Admin.DTO.Employee;

namespace StaffRecords.Admin.Mapper.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, UpdateEmployeeDTO>();
        }
    }
}
