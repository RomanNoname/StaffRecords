using Microsoft.AspNetCore.Components;
using MudBlazor;
using StaffRecords.WEB.DTO.Employee;
using StaffRecords.WEB.Requests.Interfaces;

namespace StaffRecords.WEB.Components.Employee
{
    public partial class UpdateEmployeeComponent : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public UpdateEmployeeDTO Model { get; set; }

        [Inject] IEmployeeRequests EmployeeRequests { get; set; } = default!;
        protected override void OnInitialized()
        {
            StateHasChanged();
        }


        private async void HandleValidSubmit()
        {
            await EmployeeRequests.UpdateEmployeeAsync(Model);
            MudDialog.Cancel();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
