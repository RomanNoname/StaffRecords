namespace StaffRecords.Admin.DTO.Employee
{
    public record class EmployeeDTO(string FirstName, string LastName, string Patronymic, string Address, string PhoneNumber, DateTime DateOfBirth, DateTime HireDate, decimal Salary,
     Guid DepartmentId, Guid CompanyId, Guid AppointmentId);

}
