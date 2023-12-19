using StaffRecords.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace StaffRecords.WEB.DTO.Employee
{
    public class UpdateEmployeeDTO
    {
        [Required] 
        public Guid Id { get; set;}

        [Required]
        [MaxLength(FieldsValidation.Employee.FirstNameMaxLength)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(FieldsValidation.Employee.LastNameMaxLength)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(FieldsValidation.Employee.PatronymicMaxLength)]
        public string Patronymic { get; set; }
        [Required]
        [MaxLength(FieldsValidation.Employee.AddressMaxLength)]
        public string Address { get; set; }
        [Required]
        [MaxLength(FieldsValidation.Employee.PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        [Range(0, 200000)]
        public decimal Salary { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid AppointmentId { get; set; }
    }

}
