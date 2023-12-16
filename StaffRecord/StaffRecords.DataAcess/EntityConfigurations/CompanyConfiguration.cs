using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Falidation;

namespace StaffRecords.DataAcess.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.CompanyName)
                   .HasMaxLength(FieldsValidation.Company.NameMaxLength)
                   .IsRequired();

            builder.Property(p => p.CompanyAddress)
                  .HasMaxLength(FieldsValidation.Company.AdressMaxLength)
                  .IsRequired();

        }
    }
}
