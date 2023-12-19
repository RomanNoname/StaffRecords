namespace StaffRecords.Domain.Responces.Appointment
{
    public record class GetAppointmentResponse(Guid Id, string AppointmentName, DateTime DateCreated);
}
