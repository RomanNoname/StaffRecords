namespace StaffRecords.Domain.Responces.Companies
{
    public record class GetCompanyResponse(Guid Id, string CompanyName, string CompanyAddress);
    
}
