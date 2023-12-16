namespace StraffRecords.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string DepartmentName { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
