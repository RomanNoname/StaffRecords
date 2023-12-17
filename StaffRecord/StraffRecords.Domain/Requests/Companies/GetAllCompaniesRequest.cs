using MediatR;
using StraffRecords.Domain.Responces.Companies;

namespace StraffRecords.Domain.Requests.Companies
{
    public record class GetAllCompaniesRequest():IRequest<IEnumerable<GetCompanyResponse>>;
    
}
