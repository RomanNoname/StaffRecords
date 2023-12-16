namespace StraffRecords.Domain.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
