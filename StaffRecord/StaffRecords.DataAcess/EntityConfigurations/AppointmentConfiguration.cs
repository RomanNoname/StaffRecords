using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StraffRecords.Domain.Entities;
using StraffRecords.Domain.Falidation;

namespace StaffRecords.DataAcess.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(p => p.AppointmentName)
                .HasMaxLength(FieldsValidation.Appointment.NameMaxLength)
                .IsRequired();
        }
    }
}
