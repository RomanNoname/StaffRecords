using MediatR;
using StaffRecords.Domain.Attributes;
using StaffRecords.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace StaffRecords.Domain.Requests.Employees
{
    public record class UpdateEmployeeRequest() : IRequest
    {
        [Required]
        public Guid Id { get; set; }

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
        [DateRangeValidation(FieldsValidation.Employee.MinAgeToHire, FieldsValidation.Employee.MaxAgeToHire)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DateRangeValidation(0, FieldsValidation.Employee.MaxAgeToHire)]
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
