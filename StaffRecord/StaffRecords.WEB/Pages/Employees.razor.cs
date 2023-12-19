using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using StaffRecords.WEB.Components.Employee;
using StaffRecords.WEB.DTO.Company;
using StaffRecords.WEB.DTO.Department;
using StaffRecords.WEB.DTO.Employee;
using StaffRecords.WEB.Requests.Interfaces;
using StaffRecords.Domain.QueryModels;
using System.Text;

namespace StaffRecords.WEB.Pages
{
    public partial class Employees : ComponentBase
    {
        [Inject] public IEmployeeRequests EmployeeRequests { get; set; } = default!;
        [Inject] public ICompanyRequests CompanyRequests { get; set; } = default!;
        [Inject] public IDepartmentRequests DepartmentRequests { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] IJSRuntime JSRuntime { get; set; } = default!;

        [Inject] IMapper Mapper { get; set; } = default!;

        private IEnumerable<EmployeeDTO> _employees;
        private IEnumerable<CompanyDTO> _companies;
        private IEnumerable<DeparmentDTO> _departments;
        private decimal? _totalSalary;

        private EmployeeQueryString _employeeQueryParams = new();

        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            _companies = await CompanyRequests.GetAllCompaniesAsync();
            _employees = await EmployeeRequests.GetAllEmployeesAsync();
            _departments = await DepartmentRequests.GetAllDepartmentsAsync();

            _loading = false;
            StateHasChanged();
        }

        private async void Search()
        {
            ValidateSalaries();
            _employees = await EmployeeRequests.GetEmployeesBySearchAsync(_employeeQueryParams);
            StateHasChanged();
        }
        private void ValidateSalaries()
        {
            if (_employeeQueryParams.SalaryFrom.HasValue && _employeeQueryParams.SalaryTo.HasValue &&
        _employeeQueryParams.SalaryFrom > _employeeQueryParams.SalaryTo)
            {

                _employeeQueryParams.SalaryTo = _employeeQueryParams.SalaryFrom;
            }

            _totalSalary = null;

            StateHasChanged();
        }
        private async void EditEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var updateEmployee = new UpdateEmployeeDTO();

            Mapper.Map(employeeDTO, updateEmployee);

            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true, MaxWidth = MaxWidth.Small };

            var parameter = new DialogParameters
            {
                { "Model", updateEmployee }

            };

            var dialog = await DialogService.ShowAsync<UpdateEmployeeComponent>($"Оновлення інфомрації", parameter, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                _employees = await EmployeeRequests.GetEmployeesBySearchAsync(_employeeQueryParams);
                Snackbar.Add("Оновлено!", Severity.Success);
                _totalSalary = null;
                StateHasChanged();
            }

        }
        private async Task ReportAsync()
        {

            var content = string.Join(Environment.NewLine, _employees.Select(e => $"{_companies.FirstOrDefault(c=>c.Id==e.CompanyId)?.CompanyName.PadRight(20)}" +
            $"{_departments.FirstOrDefault(c => c.Id == e.DepartmentId)?.DepartmentName.PadRight(15)}" +
            $"{e.LastName.PadRight(20)}{e.FirstName.PadRight(15)}" +
            $"{e.Salary.ToString("F2").PadRight(10)}"));

            var bytes = Encoding.UTF8.GetBytes(content);


            var file = Convert.ToBase64String(bytes);
            var uri = $"data:application/octet-stream;base64,{file}";


            await JSRuntime.InvokeVoidAsync("downloadFile", uri, "employees.txt");
        }

        private async Task GetTotalSalaryAsync()
        {
            _totalSalary = await EmployeeRequests.GetEmployeesTotalSalaryAsync(_employeeQueryParams);

        }
    }
}
