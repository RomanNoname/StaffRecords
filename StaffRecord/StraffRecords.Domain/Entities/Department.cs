namespace StraffRecords.Domain.Entities
{
    public class Department
    {
        public Guid DepartmentId { get; set; } = Guid.NewGuid();
        public string DepartmentName { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
