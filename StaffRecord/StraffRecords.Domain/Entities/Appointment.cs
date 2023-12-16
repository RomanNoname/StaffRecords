namespace StraffRecords.Domain.Entities
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public string AppointmentName { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
