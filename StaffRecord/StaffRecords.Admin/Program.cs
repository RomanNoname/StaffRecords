using Microsoft.AspNetCore.Components;
using MudBlazor.Services;
using StaffRecords.Admin.Components;
using StaffRecords.Admin.Requests;
using StaffRecords.Admin.Requests.Interfaces;
using StaffRecords.Frontend.Shared.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddresses:ApiBaseUrl"]!);
});
builder.Services.AddScoped<IHttpApiRequests, HttpRequests>(
    serviceProvider => new HttpRequests(
            factory: serviceProvider.GetRequiredService<IHttpClientFactory>(),
            navManager: serviceProvider.GetRequiredService<NavigationManager>(),
            clientName: "Api")
    );

builder.Services.AddScoped<IEmployeeRequests, EmployeeRequests>();
builder.Services.AddScoped<ICompanyRequests, CompanyRequests>();
builder.Services.AddScoped<IDepartmentRequests, DepartmentRequests>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
