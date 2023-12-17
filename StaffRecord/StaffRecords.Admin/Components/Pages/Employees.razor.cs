﻿using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using StaffRecords.Admin.Components.Employee;
using StaffRecords.Admin.DTO.Company;
using StaffRecords.Admin.DTO.Department;
using StaffRecords.Admin.DTO.Employee;
using StaffRecords.Admin.Requests.Interfaces;
using StraffRecords.Domain.SearchString;
using static MudBlazor.CategoryTypes;

namespace StaffRecords.Admin.Components.Pages
{
    public partial class Employees
    {
        [Inject] public IEmployeeRequests EmployeeRequests { get; set; } = default!;
        [Inject] public ICompanyRequests CompanyRequests { get; set; } = default!;
        [Inject] public IDepartmentRequests DepartmentRequests { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; } = default!;

        [Inject] IMapper Mapper { get; set; } = default!;

        private IEnumerable<EmployeeDTO> _employees;
        private IEnumerable<CompanyDTO> _companies;
        private IEnumerable<DeparmentDTO> _departments;

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


                StateHasChanged();
        }
        private async void EditEmployeeAsync(EmployeeDTO employeeDTO)
        {
            Snackbar.Add("Coo", Severity.Warning);
        }


    }
}