namespace StraffRecords.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public string AppointmentName { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
