using MediatR;
using Microsoft.AspNetCore.Mvc;
using StaffRecords.DataAcess;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Requests.Employees;
using StraffRecords.Domain.Responces.Employees;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {


        private readonly ILogger<StaffController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public StaffController(ILogger<StaffController> logger, ApplicationDbContext application, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _context = application;
        }

        [HttpGet]
        public async Task<GetAllEmployeeResponse> Get()
        {
            return await _mediator.Send(new GetAllEmployeeRequest());
        }
    }
}
