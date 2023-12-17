using Microsoft.AspNetCore.Components;
using StaffRecords.Admin.Requests.Interfaces;

namespace StaffRecords.Admin.Components.Pages
{
    public partial class Counter
    {
        [Inject] IEmployeeRequests EmployeeRequests { get; set; }

        private int currentCount = 0;

        private async void IncrementCount()
        {
            await EmployeeRequests.GetAllEmployeeAsync();
            currentCount++;
        }
    }


}
