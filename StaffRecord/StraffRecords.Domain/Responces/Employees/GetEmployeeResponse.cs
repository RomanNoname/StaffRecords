namespace StraffRecords.Domain.Responces.Employees
{
    public record class GetEmployeeResponse(string FirstName, string LastName, string Patronymic, string Address, string PhoneNumber, DateTime DateOfBirth, DateTime HireDate, decimal Salary,
     Guid DepartmentId, Guid CompanyId, Guid AppointmentId);
}
