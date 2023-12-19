using Bogus;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StaffRecords.Domain.Entities;
using StaffRecords.Domain.Validation;

namespace StaffRecords.DataInitialisation
{
    public class InitialSQLService : BackgroundService
    {
        private readonly string _dbConnectionString;
        private readonly string _dataBaseName;
        private readonly Faker _faker = new();
        private readonly ILogger<InitialSQLService> _logger;

        public InitialSQLService(string dbConnectionString, string dataBaseName, ILogger<InitialSQLService> logger)
        {
            _dbConnectionString = dbConnectionString;
            _dataBaseName = dataBaseName;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var departments = new List<Department>() {

                new Department() {DepartmentName = "HR", Id = Guid.NewGuid()},
                  new Department() {DepartmentName = "PM", Id = Guid.NewGuid()},
                    new Department() {DepartmentName = "Backend", Id = Guid.NewGuid()},
                      new Department() {DepartmentName = "Frontend", Id = Guid.NewGuid()}
            };

            var companies = new List<Company>()
            { new Company() {CompanyName = "New World", Id = Guid.NewGuid(), CompanyAddress="Poltava"},
                  new Company() {CompanyName = "Ananas", Id = Guid.NewGuid(), CompanyAddress="Ananas"},
                    new Company() {CompanyName = "White Tower", Id = Guid.NewGuid(), CompanyAddress="Kharkiv"},
                      new Company() {CompanyName = "Eco", Id = Guid.NewGuid(), CompanyAddress="Kiev" }
                    };

            var appointments = new List<Appointment>()
            { new Appointment() {AppointmentName = "Project Manager", Id = Guid.NewGuid()},
                  new Appointment() {AppointmentName = "HR", Id = Guid.NewGuid()},
                    new Appointment() {AppointmentName = "FullStack Developer", Id = Guid.NewGuid()},
                      new Appointment() {AppointmentName = "JS Developer", Id = Guid.NewGuid() },
                        new Appointment() {AppointmentName = ".NET Developer", Id = Guid.NewGuid() }
                    };


            try
            {
                await CreatingDatabaseAsync();
                await CreatDepartmentTableAsync(stoppingToken);
                await CreatCompanyTableAsync(stoppingToken);
                await CreatAppointmentTableAsync(stoppingToken);
                await CreatEmployeeTableAsync(stoppingToken);
                await SeedDepartmentsAsync(stoppingToken, departments);
                await SeedAppointmentsAsync(stoppingToken, appointments);
                await SeedCompaniesAsync(stoppingToken, companies);
                await SeedEmployeesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unhandled exceptions occurred while migrating database");
            }

        }
        private async Task CreatingDatabaseAsync()
        {
            using var masterConnection = new SqlConnection(_dbConnectionString);

            masterConnection.Open();

            using var createDatabaseCommand = masterConnection.CreateCommand();

            createDatabaseCommand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dataBaseName}') CREATE DATABASE {_dataBaseName}";
            createDatabaseCommand.ExecuteNonQuery();
        }
        private async Task CreatDepartmentTableAsync(CancellationToken cancellationToken)
        {
            if (await ExistTableAsync("Department", cancellationToken))
            {
                return;
            }

            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);
            using var command = connection.CreateCommand();
            command.CommandText = $@"
                    USE {_dataBaseName}
                        CREATE TABLE {_dataBaseName}.dbo.Department (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            DepartmentName NVARCHAR(MAX),
                            DateCreated DATETIME,
                            DateUpdated DATETIME)";
            await command.ExecuteNonQueryAsync();

        }

        private async Task CreatCompanyTableAsync(CancellationToken cancellationToken)
        {
            if (await ExistTableAsync("Company", cancellationToken))
            {
                return;
            }

            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);
            using var command = connection.CreateCommand();

            command.CommandText = $@"
                   USE {_dataBaseName}
                        CREATE TABLE {_dataBaseName}.dbo.Company (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            CompanyName NVARCHAR({FieldsValidation.Company.NameMaxLength}),
                            CompanyAddress NVARCHAR(MAX),
                            DateCreated DATETIME,
                            DateUpdated DATETIME)";
            await command.ExecuteNonQueryAsync();
        }

        private async Task CreatAppointmentTableAsync(CancellationToken cancellationToken)
        {
            if (await ExistTableAsync("Appointment", cancellationToken))
            {
                return;
            }

            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();

            command.CommandText = $@"
                    USE {_dataBaseName}
                    CREATE TABLE {_dataBaseName}.dbo.Appointment (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            AppointmentName NVARCHAR({FieldsValidation.Appointment.NameMaxLength}) NOT NULL CHECK (LEN(AppointmentName) <= {FieldsValidation.Appointment.NameMaxLength}),
                            DateCreated DATETIME,
                            DateUpdated DATETIME)";

            await command.ExecuteNonQueryAsync();

        }

        private async Task CreatEmployeeTableAsync(CancellationToken cancellationToken)
        {
            if (await ExistTableAsync("Employee", cancellationToken))
            {
                return;
            }

            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);
            using var command = connection.CreateCommand();

            command.CommandText = $@"
                   CREATE TABLE {_dataBaseName}.dbo.Employee (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            FirstName NVARCHAR(MAX),
                            LastName NVARCHAR(MAX),
                            Patronymic NVARCHAR(MAX),
                            Address NVARCHAR(MAX),
                            PhoneNumber NVARCHAR(MAX),
                            DateOfBirth DATETIME,
                            HireDate DATETIME,
                            Salary DECIMAL,
                            DepartmentId UNIQUEIDENTIFIER,
                            CompanyId UNIQUEIDENTIFIER,
                            AppointmentId UNIQUEIDENTIFIER,
                            DateCreated DATETIME,
                            DateUpdated DATETIME,
                            FOREIGN KEY (DepartmentId) REFERENCES {_dataBaseName}.dbo.Department(Id),
                            FOREIGN KEY (CompanyId) REFERENCES {_dataBaseName}.dbo.Company(Id),
                            FOREIGN KEY (AppointmentId) REFERENCES {_dataBaseName}.dbo.Appointment(Id)
                        )";
            await command.ExecuteNonQueryAsync();

        }

        private async Task SeedDepartmentsAsync(CancellationToken stoppingToken, List<Department> departments)
        {
            using var connection = new SqlConnection(_dbConnectionString);
            await connection.OpenAsync(stoppingToken);

            using var command = connection.CreateCommand();

            if (await ExistDataInTableAsync("Department", stoppingToken))
            {
                return;
            }

            var parameters = new List<string>();
            for (int i = 0; i < departments.Count; i++)
            {
                var department = departments[i];
                parameters.Add($"(@Id{i}, @DepartmentName{i}, GETUTCDATE(), GETUTCDATE())");


                command.Parameters.AddWithValue($"@Id{i}", department.Id);
                command.Parameters.AddWithValue($"@DepartmentName{i}", department.DepartmentName);
            }

            var values = string.Join(",", parameters);

            command.CommandText = $@"
                USE {_dataBaseName}
               
                    INSERT INTO Department (Id, DepartmentName, DateCreated, DateUpdated)
                    VALUES {values}";

            await command.ExecuteNonQueryAsync(stoppingToken);
        }

        private async Task SeedAppointmentsAsync(CancellationToken stoppingToken, List<Appointment> appointments)
        {
            using var connection = new SqlConnection(_dbConnectionString);
            await connection.OpenAsync(stoppingToken);

            using var command = connection.CreateCommand();

            if (await ExistDataInTableAsync("Appointment", stoppingToken))
            {
                return;
            }

            var parameters = new List<string>();
            for (int i = 0; i < appointments.Count; i++)
            {
                var appointment = appointments[i];
                parameters.Add($"(NEWID(), @AppointmentName{i}, GETUTCDATE(), GETUTCDATE())");


                command.Parameters.AddWithValue($"@AppointmentName{i}", appointment.AppointmentName);
            }

            var values = string.Join(",", parameters);

            command.CommandText = $@"
                USE {_dataBaseName}
                INSERT INTO Appointment (Id, AppointmentName, DateCreated, DateUpdated)
                VALUES {values}";

            await command.ExecuteNonQueryAsync(stoppingToken);

        }


        private async Task SeedCompaniesAsync(CancellationToken stoppingToken, List<Company> companies)
        {
            using var connection = new SqlConnection(_dbConnectionString);
            await connection.OpenAsync(stoppingToken);

            using var command = connection.CreateCommand();

            if (await ExistDataInTableAsync("Company", stoppingToken))
            {
                return;
            }

            var parameters = new List<string>();
            for (int i = 0; i < companies.Count; i++)
            {
                var company = companies[i];
                parameters.Add($"(NEWID(), @CompanyName{i}, @CompanyAddress{i}, GETUTCDATE(), GETUTCDATE())");


                command.Parameters.AddWithValue($"@CompanyName{i}", company.CompanyName);
                command.Parameters.AddWithValue($"@CompanyAddress{i}", company.CompanyAddress);
            }

            var values = string.Join(",", parameters);

            command.CommandText = $@"
                USE {_dataBaseName}
                INSERT INTO Company (Id, CompanyName, CompanyAddress, DateCreated, DateUpdated)
                VALUES  {values}";

            await command.ExecuteNonQueryAsync(stoppingToken);

        }

        private async Task SeedEmployeesAsync(CancellationToken stoppingToken)
        {
            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(stoppingToken);

            var appointmentGuids = new List<Guid>();
            var deparmentGuids = new List<Guid>();
            var companyGuids = new List<Guid>();

            using var command = connection.CreateCommand();

            if (await ExistDataInTableAsync("Employee", stoppingToken))
            {
                return;
            }


            command.CommandText = $"USE {_dataBaseName} SELECT Id FROM Appointment";
            using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
            {
                while (await reader.ReadAsync(stoppingToken))
                {
                    appointmentGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                }
            }

            command.CommandText = $"USE {_dataBaseName} SELECT Id FROM Company";
            using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
            {
                while (await reader.ReadAsync(stoppingToken))
                {
                    companyGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                }
            }

            command.CommandText = $"USE {_dataBaseName} SELECT Id FROM Department";
            using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
            {
                while (await reader.ReadAsync(stoppingToken))
                {
                    deparmentGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                }
            }

            for (int i = 0; i < 12; i++)
            {
                var employee = new StaffRecords.Domain.Entities.Employee()
                {
                    Id = Guid.NewGuid(),
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    Patronymic = _faker.Internet.UserName(),
                    PhoneNumber = $"{_faker.Random.Int(0, 9)}{_faker.Random.Int(0, 9)}{_faker.Random.Int(0, 9)}1222333444",
                    Address = _faker.Address.StreetAddress(),
                    DepartmentId = deparmentGuids[_faker.Random.Int(0, deparmentGuids.Count() - 1)],
                    AppointmentId = appointmentGuids[_faker.Random.Int(0, appointmentGuids.Count() - 1)],
                    CompanyId = companyGuids[_faker.Random.Int(0, companyGuids.Count() - 1)],
                    DateOfBirth = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                    HireDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                    Salary = _faker.Random.Decimal(1, 200000)
                };
                command.CommandText = $@"USE {_dataBaseName} INSERT INTO Employee (Id, FirstName, LastName, Patronymic, PhoneNumber, Address, DateOfBirth, HireDate, Salary, CompanyId, DepartmentId, AppointmentId, DateCreated, DateUpdated)
                        VALUES (
                            '{employee.Id}','{employee.FirstName}','{employee.LastName}','{employee.Patronymic}','{employee.PhoneNumber}',
                             '{employee.Address}','{employee.DateOfBirth.ToString("yyyy-MM-dd")}',
                        '{employee.HireDate.ToString("yyyy-MM-dd")}',{employee.Salary.ToString().Replace(',', '.')},'{employee.CompanyId}','{employee.DepartmentId}','{employee.AppointmentId}',
                         '{employee.DateCreated.ToString("yyyy-MM-dd")}','{employee.DateUpdated.ToString("yyyy-MM-dd")}')";

                await command.ExecuteNonQueryAsync(stoppingToken);
            }

        }

        private async Task<bool> ExistTableAsync(string tableName, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();

            command.CommandText = $@"
                    USE {_dataBaseName}
                    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}')
                        SELECT 1
                    ELSE
                        SELECT 0";

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt16(result) == 1;

        }

        private async Task<bool> ExistDataInTableAsync(string tableName, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_dbConnectionString);

            await connection.OpenAsync(cancellationToken);

            using var command = connection.CreateCommand();

            command.CommandText = $"USE {_dataBaseName} IF EXISTS (SELECT TOP(1) * FROM {tableName}) SELECT 1 ELSE SELECT 0";
            var result = await command.ExecuteScalarAsync(cancellationToken);

            return Convert.ToInt16(result) == 1;
        }

    }
}
