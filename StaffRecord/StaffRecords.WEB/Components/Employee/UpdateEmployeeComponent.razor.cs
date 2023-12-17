using Microsoft.AspNetCore.Components;
using MudBlazor;
using StaffRecords.WEB.DTO.Employee;

namespace StaffRecords.WEB.Components.Employee
{
    public partial class UpdateEmployeeComponent : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public UpdateEmployeeDTO Model { get; set; }


        protected override void OnInitialized()
        {
            StateHasChanged();
        }
       

        private void HandleValidSubmit()
        {
        }
    }
}
