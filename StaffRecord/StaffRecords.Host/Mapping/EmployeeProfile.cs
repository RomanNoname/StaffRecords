using AutoMapper;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Requests.Employees;
using StaffRecords.Domain.Responces.Employees;

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
