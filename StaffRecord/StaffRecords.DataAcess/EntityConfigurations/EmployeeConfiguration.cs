using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Falidation;

namespace StaffRecords.DataAcess.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.FirstName)
                .HasMaxLength(FieldsValidation.Employee.FirstNameMaxLength)
                .IsRequired();
            builder.Property(p => p.LastName)
               .HasMaxLength(FieldsValidation.Employee.LastNameMaxLength)
               .IsRequired();
            builder.Property(p => p.Patronymic)
               .HasMaxLength(FieldsValidation.Employee.PatronymicMaxLength)
               .IsRequired();
            builder.Property(p => p.Address)
               .HasMaxLength(FieldsValidation.Employee.AddressMaxLength)
               .IsRequired();

            builder.Property(p=>p.Salary)
                .HasColumnType("decimal(15,2)");
           
            builder.HasCheckConstraint($"{nameof(Employee.Salary)}", $"{nameof(Employee.Salary)}>{FieldsValidation.Employee.MinSalary} AND {nameof(Employee.Salary)}<{FieldsValidation.Employee.MaxSalary}");

            builder.HasCheckConstraint($"{nameof(Employee.PhoneNumber)}", $"LEN({nameof(Employee.PhoneNumber)}) = {FieldsValidation.Employee.PhoneNumberMaxLength} AND {nameof(Employee.PhoneNumber)} NOT LIKE '%[^0-9]%'");

        }
    }
}
