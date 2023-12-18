using Microsoft.EntityFrameworkCore;
using StaffRecords.DA;
using StaffRecords.DataAcess;
using StaffRecords.Handlers;
using StaffRecords.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)))
);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService(provider => new InitialSQLService(
    builder.Configuration.GetConnectionString("ApplicationDbContext"),
    builder.Configuration.GetConnectionString("DataBase"),
    provider.GetRequiredService<ILogger<InitialSQLService>>()));


builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(HandlersAssemblyMarker).Assembly));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
