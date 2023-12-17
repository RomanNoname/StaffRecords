using Microsoft.AspNetCore.Components;
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
