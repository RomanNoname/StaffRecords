using StaffRecords.DataInitialisation;
using StaffRecords.Domain.Entities;
using StaffRecords.Repository.Contracts.IRepositories;

namespace StaffRecords.Repository.Implementation.Repositories
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }
    }
}
