using Microsoft.AspNetCore.Mvc;
using StaffRecords.DataAcess;
using StraffRecords.Domain.Entities;

namespace StaffRecords.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
      

        private readonly ILogger<StaffController> _logger;
        private readonly ApplicationDbContext _context;

        public StaffController(ILogger<StaffController> logger, ApplicationDbContext application)
        {
            _logger = logger;

            _context = application;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
          return _context.Employees;
        }
    }
}
