using StaffRecords.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace StaffRecords.WEB.DTO.Employee
{
    public class UpdateEmployeeDTO
    {
        [Required(ErrorMessage = "Треба заповнити")]
        public Guid Id { get; set;}

        [Required(ErrorMessage = "Треба заповнити")]
        [MaxLength(FieldsValidation.Employee.FirstNameMaxLength, ErrorMessage = $"Максимальна довжина: 30")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        [MaxLength(FieldsValidation.Employee.LastNameMaxLength, ErrorMessage = $"Максимальна довжина: 30")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        [MaxLength(FieldsValidation.Employee.PatronymicMaxLength, ErrorMessage = $"Максимальна довжина: 30")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        [MaxLength(FieldsValidation.Employee.AddressMaxLength, ErrorMessage = $"Максимальна довжина: 120")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        [MaxLength(FieldsValidation.Employee.PhoneNumberMaxLength, ErrorMessage = $"Максимальна довжина: 13")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        [Range(0, 200000, ErrorMessage = $"Діапазон між 0 та 200 000")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Треба заповнити")]
        public Guid AppointmentId { get; set; }
    }

}
