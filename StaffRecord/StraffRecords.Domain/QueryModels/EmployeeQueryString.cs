﻿namespace StaffRecords.Domain.QueryModels
{
    public class EmployeeQueryString
    {
        public string? LastName { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public string? DepartmentName { get; set; } = string.Empty;

        public decimal? SalaryFrom { get; set; }

        public decimal? SalaryTo { get; set; }

        public DateTime? DateHireFrom { get; set; }
        public DateTime? DateHireTo { get; set; }
    }
}
