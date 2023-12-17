using MediatR;
using Microsoft.AspNetCore.Mvc;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {

        private readonly IMediator _mediator;

        public StaffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetEmployeeResponse>> Get()
        {
            return await _mediator.Send(new GetAllEmployeeRequest());
        }
    }
}
