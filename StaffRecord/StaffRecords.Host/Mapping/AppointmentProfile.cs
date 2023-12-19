using AutoMapper;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Responces.Appointment;

namespace StaffRecords.Host.Mapping
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentResponse>();
        }
    }
}
