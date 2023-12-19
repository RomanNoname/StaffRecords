using MediatR;
using StaffRecords.Domain.Responces.Companies;

namespace StaffRecords.Domain.Requests.Companies
{
    public record class GetAllCompaniesRequest():IRequest<IEnumerable<GetCompanyResponse>>;
    
}
