using Bogus;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StraffRecords.Domain.Falidation;

namespace StaffRecords.DataAcess
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
            try
            {
                await CreatingTablesAsync();
                await SeedDepartmentsAsync(stoppingToken);
                await SeedAppointmentsAsync(stoppingToken);
                await SeedCompaniesAsync(stoppingToken);
                await SeedEmployeesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unhandled exceptions occurred while migrating database");
                return;
            }


        }

        private async Task CreatingTablesAsync()
        {

            using (SqlConnection masterConnection = new SqlConnection(_dbConnectionString))
            {
                masterConnection.Open();

                using (SqlCommand createDatabaseCommand = masterConnection.CreateCommand())
                {
                    createDatabaseCommand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{_dataBaseName}') CREATE DATABASE {_dataBaseName}";
                    createDatabaseCommand.ExecuteNonQuery();
                }
            }

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                    USE {_dataBaseName}
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Appointment')
                    BEGIN
                        CREATE TABLE {_dataBaseName}.dbo.Appointment (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            AppointmentName NVARCHAR({FieldsValidation.Appointment.NameMaxLength}) NOT NULL CHECK (LEN(AppointmentName) <= {FieldsValidation.Appointment.NameMaxLength}),
                            DateCreated DATETIME,
                            DateUpdated DATETIME
                        )
                    END
                    ";

                    await command.ExecuteNonQueryAsync();


                    command.CommandText = $@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Company')
                    BEGIN
                        CREATE TABLE {_dataBaseName}.dbo.Company (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            CompanyName NVARCHAR({FieldsValidation.Company.NameMaxLength}),
                            CompanyAddress NVARCHAR(MAX),
                            DateCreated DATETIME,
                            DateUpdated DATETIME
                        )
                    END";
                    await command.ExecuteNonQueryAsync();


                    command.CommandText = $@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Department')
                    BEGIN
                        CREATE TABLE {_dataBaseName}.dbo.Department (
                            Id UNIQUEIDENTIFIER PRIMARY KEY,
                            DepartmentName NVARCHAR(MAX),
                            DateCreated DATETIME,
                            DateUpdated DATETIME
                        )
                    END";
                    await command.ExecuteNonQueryAsync();


                    command.CommandText = $@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Employee')
                    BEGIN
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
                        )
                    END";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task SeedDepartmentsAsync(CancellationToken stoppingToken)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync(stoppingToken);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                    Use {_dataBaseName}
                    IF NOT EXISTS (SELECT TOP(1) * FROM Department)
                    BEGIN
                        INSERT INTO Department (Id, DepartmentName, DateCreated, DateUpdated)
                        VALUES
                            (NEWID(), 'HR', GETUTCDATE(), GETUTCDATE()),
                            (NEWID(), 'Backend', GETUTCDATE(), GETUTCDATE()),
                            (NEWID(), 'PM', GETUTCDATE(), GETUTCDATE()),
                            (NEWID(), 'Frontend', GETUTCDATE(), GETUTCDATE())
                    END";

                    await command.ExecuteNonQueryAsync(stoppingToken);
                }
            }
        }
        private async Task SeedAppointmentsAsync(CancellationToken stoppingToken)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync(stoppingToken);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                USE {_dataBaseName}
                IF NOT EXISTS (SELECT TOP(1) * FROM Appointment)
                BEGIN
                    INSERT INTO Appointment (Id, AppointmentName, DateCreated, DateUpdated)
                    VALUES
                        (NEWID(), 'JS Developer', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), '.NET Developer', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'FullStack Developer', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'HR', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'Project Manager', GETUTCDATE(), GETUTCDATE())
                END";

                    await command.ExecuteNonQueryAsync(stoppingToken);
                }
            }
        }

        private async Task SeedCompaniesAsync(CancellationToken stoppingToken)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync(stoppingToken);

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                USE {_dataBaseName}
                IF NOT EXISTS (SELECT TOP(1) * FROM Company)
                BEGIN
                    INSERT INTO Company (Id, CompanyName, CompanyAddress, DateCreated, DateUpdated)
                    VALUES
                        (NEWID(), 'Ananas', 'Poltava', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'ECO', 'Kyiv', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'New World', 'Kharkiv', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'White Tower', 'Dnipro', GETUTCDATE(), GETUTCDATE()),
                        (NEWID(), 'Nice', 'Lviv', GETUTCDATE(), GETUTCDATE())
                END";

                    await command.ExecuteNonQueryAsync(stoppingToken);
                }
            }
        }

        private async Task SeedEmployeesAsync(CancellationToken stoppingToken)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                await connection.OpenAsync(stoppingToken);

                var appointmentGuids = new List<Guid>();
                var deparmentGuids = new List<Guid>();
                var companyGuids = new List<Guid>();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"USE {_dataBaseName} IF EXISTS (SELECT TOP(1) * FROM Employee) SELECT 1 ELSE SELECT 0";
                    var result = await command.ExecuteScalarAsync(stoppingToken);

                    if (Convert.ToInt32(result) == 1)
                        return;


                    command.CommandText = $"SELECT Id FROM Appointment";
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
                    {
                        while (await reader.ReadAsync(stoppingToken))
                        {
                            appointmentGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                        }
                    }

                    command.CommandText = $"SELECT Id FROM Company";
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
                    {
                        while (await reader.ReadAsync(stoppingToken))
                        {
                            companyGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                        }
                    }

                    command.CommandText = $"SELECT Id FROM Department";
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(stoppingToken))
                    {
                        while (await reader.ReadAsync(stoppingToken))
                        {
                            deparmentGuids.Add(reader.GetGuid(reader.GetOrdinal("Id")));
                        }
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        var employee = new StraffRecords.Domain.Entities.Employee()
                        {
                            Id = Guid.NewGuid(),
                            FirstName = _faker.Name.FirstName(),
                            LastName = _faker.Name.LastName(),
                            Patronymic = _faker.Internet.UserName(),
                            PhoneNumber = $"{_faker.Random.Int(0, 9)}111222333444",
                            Address = _faker.Address.StreetAddress(),
                            DepartmentId = deparmentGuids[_faker.Random.Int(0, deparmentGuids.Count() - 1)],
                            AppointmentId = appointmentGuids[_faker.Random.Int(0, appointmentGuids.Count() - 1)],
                            CompanyId = companyGuids[_faker.Random.Int(0, companyGuids.Count() - 1)],
                            DateOfBirth = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                            HireDate = DateTime.UtcNow.AddYears(-_faker.Random.Int(0, 20)),
                            Salary = _faker.Random.Decimal(1, 200000)
                        };
                        command.CommandText = $@"INSERT INTO Employee (Id, FirstName, LastName, Patronymic, PhoneNumber, Address, DateOfBirth, HireDate, Salary, CompanyId, DepartmentId, AppointmentId, DateCreated, DateUpdated)
                        VALUES (
                            '{employee.Id}','{employee.FirstName}','{employee.LastName}','{employee.Patronymic}','{employee.PhoneNumber}',
                             '{employee.Address}','{employee.DateOfBirth.ToString("yyyy-MM-dd")}',
                        '{employee.HireDate.ToString("yyyy-MM-dd")}',{employee.Salary.ToString().Replace(',', '.')},'{employee.CompanyId}','{employee.DepartmentId}','{employee.AppointmentId}',
                         '{employee.DateCreated.ToString("yyyy-MM-dd")}','{employee.DateUpdated.ToString("yyyy-MM-dd")}')";

                        await command.ExecuteNonQueryAsync(stoppingToken);
                    }

                }

            }
        }


    }
}
