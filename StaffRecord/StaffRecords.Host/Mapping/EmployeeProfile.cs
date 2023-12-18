using AutoMapper;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Host.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, GetEmployeeResponse>();
            CreateMap<UpdateEmployeeRequest, Employee>();
        }
    }
}
