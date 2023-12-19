using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffRecords.Domain.Requests.Companies;
using StaffRecords.Domain.Responces.Companies;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<GetCompanyResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllCompaniesRequest(), cancellationToken);
        }
    }
}
