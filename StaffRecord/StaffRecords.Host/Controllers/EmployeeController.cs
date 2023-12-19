using MediatR;
using Microsoft.AspNetCore.Mvc;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;
using StraffRecords.Domain.SearchString;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<GetEmployeeResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllEmployeesRequest(), cancellationToken);
        }

        [HttpGet("search")]
        public async Task<IEnumerable<GetEmployeeResponse>> Search([FromQuery] EmployeeQueryString queryString, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetEmployeesBySearchRequest(queryString), cancellationToken);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }

        [HttpGet("totalSalary")]
        public async Task<decimal> GetTotalSalary([FromQuery] EmployeeQueryString employeeQueryString, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetEmployeesTotalSalaryRequest(employeeQueryString), cancellationToken);
        }
    }
}
