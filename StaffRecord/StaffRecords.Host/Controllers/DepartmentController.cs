using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffRecords.Domain.Requests.Departments;
using StaffRecords.Domain.Responces.Departments;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<GetDepartmentResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllDepartmentsRequest(), cancellationToken);
        }
    }
}
