using AutoMapper;
using MediatR;
using StaffRecords.Domain.Requests.Appointment;
using StaffRecords.Domain.Responces.Appointment;
using StaffRecords.Repository.Contracts.IRepositories;

namespace StaffRecords.Handlers.AppointmentHandlers
{
    public class GetAllAppointmentsHandler : IRequestHandler<GetllAllAppointmentsRequest, IEnumerable<GetAppointmentResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAllAppointmentsHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<GetAppointmentResponse>> Handle(GetllAllAppointmentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _appointmentRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<GetAppointmentResponse>>(result);
        }
    }
}
