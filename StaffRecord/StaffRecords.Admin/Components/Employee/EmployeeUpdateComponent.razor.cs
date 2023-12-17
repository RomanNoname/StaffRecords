using Microsoft.AspNetCore.Components;
using MudBlazor;
using StaffRecords.Admin.DTO.Employee;

namespace StaffRecords.Admin.Components.Employee
{
    public partial class EmployeeUpdateComponent : ComponentBase
    {
    
        [Parameter] public UpdateEmployeeDTO Model { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
    }
}
