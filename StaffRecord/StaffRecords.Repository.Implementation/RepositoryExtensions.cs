using Microsoft.Extensions.DependencyInjection;
using StaffRecords.Repository.Contracts.IRepositories;
using StaffRecords.Repository.Implementation.Repositories;

namespace StaffRecords.Repository.Implementation
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository,CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            return services;
        }
    }
}
