namespace StraffRecords.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
