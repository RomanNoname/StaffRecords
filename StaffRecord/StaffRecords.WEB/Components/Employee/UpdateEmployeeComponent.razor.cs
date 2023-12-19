using Microsoft.AspNetCore.Components;
using MudBlazor;
using StaffRecords.WEB.DTO.Appointment;
using StaffRecords.WEB.DTO.Company;
using StaffRecords.WEB.DTO.Department;
using StaffRecords.WEB.DTO.Employee;
using StaffRecords.WEB.Requests.Interfaces;

namespace StaffRecords.WEB.Components.Employee
{
    public partial class UpdateEmployeeComponent : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public UpdateEmployeeDTO Model { get; set; }
        [Parameter] public List<DeparmentDTO> DeparmentDTOs { get; set; } = new();
        [Parameter] public List<AppointmentDTO> AppointmentDTOs { get; set; } = new();
        [Parameter] public List<CompanyDTO> CompanyDTOs { get; set; } = new();

        [Inject] IEmployeeRequests EmployeeRequests { get; set; } = default!;
        protected override void OnInitialized()
        {
            StateHasChanged();
        }


        private async void HandleValidSubmit()
        {
            await EmployeeRequests.UpdateEmployeeAsync(Model);
            MudDialog!.Close();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
