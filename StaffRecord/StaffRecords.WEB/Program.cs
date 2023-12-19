using Microsoft.AspNetCore.Components;
using MudBlazor.Services;
using StaffRecords.WEB.Requests;
using StaffRecords.WEB.Requests.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

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
builder.Services.AddScoped<IAppointmentRequests, AppointmentRequests>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
