using AutoMapper;
using StaffRecords.WEB.DTO.Employee;


namespace StaffRecords.WEB.Mapper.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, UpdateEmployeeDTO>();
        }
    }
}
