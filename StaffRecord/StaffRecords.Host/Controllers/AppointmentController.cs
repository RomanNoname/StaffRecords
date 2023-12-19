using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffRecords.Domain.Requests.Appointment;
using StaffRecords.Domain.Responces.Appointment;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<GetAppointmentResponse>> Get(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetllAllAppointmentsRequest(), cancellationToken);
        }
    }
}
