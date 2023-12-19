namespace StaffRecords.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CompanyId { get; set; }

        public Guid AppointmentId { get; set; }

        public Appointment? Appointment { get; set; }
        public Department? Department { get; set; }
        public Company? Company { get; set; }
    }
}
