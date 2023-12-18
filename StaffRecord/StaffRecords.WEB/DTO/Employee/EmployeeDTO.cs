namespace StaffRecords.WEB.DTO.Employee
{
    public record class EmployeeDTO(Guid Id, string FirstName, string LastName, string Patronymic, string Address, string PhoneNumber, DateTime DateOfBirth, DateTime HireDate, decimal Salary,
     Guid DepartmentId, Guid CompanyId, Guid AppointmentId);

}
