using MediatR;
using StaffRecords.Domain.Responces.Appointment;

namespace StaffRecords.Domain.Requests.Appointment
{
    public record class GetllAllAppointmentsRequest():IRequest<IEnumerable<GetAppointmentResponse>>;
}
