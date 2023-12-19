using Newtonsoft.Json;
using StaffRecords.WEB.DTO.Appointment;
using StaffRecords.WEB.Requests.Interfaces;

namespace StaffRecords.WEB.Requests
{
    public class AppointmentRequests : IAppointmentRequests
    {
        private readonly IHttpApiRequests _httpApiRequests;

        private const string SendEndpoint = "api/Appointment";

        public AppointmentRequests(IHttpApiRequests httpApiRequests)
        {
            _httpApiRequests = httpApiRequests;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            var response = await _httpApiRequests.SendGetAsyncRequest($"{SendEndpoint}/all");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<AppointmentDTO>>(content)!;
        }
    }
}
