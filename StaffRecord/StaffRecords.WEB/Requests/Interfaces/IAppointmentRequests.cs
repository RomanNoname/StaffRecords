using StaffRecords.WEB.DTO.Appointment;

namespace StaffRecords.WEB.Requests.Interfaces
{
    public interface IAppointmentRequests
    {
        public Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();
    }
}
