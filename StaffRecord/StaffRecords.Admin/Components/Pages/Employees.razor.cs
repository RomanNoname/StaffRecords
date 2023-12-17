using Microsoft.AspNetCore.Components;
using StaffRecords.Admin.DTO.Company;
using StaffRecords.Admin.DTO.Employee;
using StaffRecords.Admin.Requests.Interfaces;

namespace StaffRecords.Admin.Components.Pages
{
    public partial class Employees
    {
        [Inject] IEmployeeRequests EmployeeRequests { get; set; } = default!;
        [Inject] ICompanyRequests CompanyRequests { get; set; } = default!;

        private IEnumerable<EmployeeDTO> _employees;
        private IEnumerable<CompanyDTO> _companies;

        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            _companies = await CompanyRequests.GetAllCompaniesAsync();
            _employees = await EmployeeRequests.GetAllEmployeesAsync();
            _loading = false;

            StateHasChanged();
        }
    }
}
