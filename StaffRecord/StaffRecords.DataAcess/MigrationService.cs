using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StraffRecords.Domain.Entities;

namespace StaffRecords.DataAcess
{
    public class MigrationsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Faker _faker = new Faker();
        private readonly ILogger<MigrationsService> _logger;

        public MigrationsService(IServiceProvider serviceProvider, ILogger<MigrationsService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //using var scope = _serviceProvider.CreateScope();
            //await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
            //try
            //{
            //    await context.Database.MigrateAsync(stoppingToken);
            //}
            //catch (Exception e)
            //{
            //    _logger.LogCritical(e, "Unhandled exceptions occurred while migrating database");
            //    return;
            //}

            //await SeedDepartmentsAsync(context, stoppingToken);
            //await SeedAppointmentsAsync(context, stoppingToken);
            //await SeedCompanieAsync(context, stoppingToken);
            //await SeedEmployeesAsync(context, stoppingToken);

        }
        private async Task SeedDepartmentsAsync(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            var deparments = new List<Department>
            {
                new Department { DepartmentName = "HR" },
                new Department { DepartmentName = "Backend" },
                new Department { DepartmentName = "PM" },
                new Department { DepartmentName = "Frontend" },
            };
            if (!context.Departments.Any())
            {
                await context.Departments.AddRangeAsync(deparments, stoppingToken);
                await context.SaveChangesAsync();
            }

        }
        private async Task SeedAppointmentsAsync(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            var appointments = new List<Appointment>
            {
                new Appointment { AppointmentName = "JS Developer" },
                new Appointment { AppointmentName = ".NET Developer" },
                new Appointment { AppointmentName = "FullStack Developer" },
                new Appointment { AppointmentName = "HR" },
                new Appointment { AppointmentName = "Project Manager" }
            };
            if (!context.Appointments.Any())
            {
                await context.Appointments.AddRangeAsync(appointments, stoppingToken);
                await context.SaveChangesAsync();
            }

        }
        private async Task SeedCompanieAsync(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            var companies = new List<Company>
            {
                new Company { CompanyName = "Ananas", CompanyAddress = "Poltava" },
                new Company { CompanyName = "ECO", CompanyAddress="Kyiv"},
                new Company { CompanyName = "New World", CompanyAddress = "Kharkiv" },
                new Company { CompanyName = "White Tower", CompanyAddress = "Dnipro" },
                new Company { CompanyName = "Nice", CompanyAddress = "Lviv" }
            };
            if (!context.Companies.Any())
            {
                await context.Companies.AddRangeAsync(companies, stoppingToken);
                await context.SaveChangesAsync();
            }

        }
        private async Task SeedEmployeesAsync(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            var companieGuids = context.Companies.Select(x => x.Id).ToList();
            var deparmentGuids = context.Departments.Select(x => x.Id).ToList();
            var appointmentGuids = context.Appointments.Select(x => x.Id).ToList();

            var employees = new List<Employee>();
            for (int i = 0; i < 9; i++)
            {
                employees.Add(new Employee()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    Patronymic = _faker.Internet.UserName(),
                    PhoneNumber = $"{_faker.Random.Int(0, 9)}111222333444",
                    Address = _faker.Address.StreetAddress(),
                    DepartmentId = deparmentGuids[_faker.Random.Int(0, deparmentGuids.Count() - 1)],
                    AppointmentId = appointmentGuids[_faker.Random.Int(0, appointmentGuids.Count() - 1)],
                    CompanyId = companieGuids[_faker.Random.Int(0, companieGuids.Count() - 1)],
                    DateOfBirth = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                    HireDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                    Salary = _faker.Random.Decimal(1, 200000)
                }
            );
            }

            if (!context.Employees.Any())
            {
                await context.Employees.AddRangeAsync(employees, stoppingToken);
                await context.SaveChangesAsync();
            }

        }
    }

}
