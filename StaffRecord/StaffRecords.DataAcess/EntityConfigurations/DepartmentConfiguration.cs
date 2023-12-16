using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Falidation;

namespace StaffRecords.DataAcess.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(p => p.DepartmentName)
                  .HasMaxLength(FieldsValidation.Department.NameMaxLength)
                  .IsRequired();
        }
    }
}
