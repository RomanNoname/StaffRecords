using StaffRecords.DataAcess;
using StaffRecords.Handlers;
using StaffRecords.Host.Middlewares;
using StaffRecords.Repository.Implementation;
using ConnectionInfo = StaffRecords.DatainItialisation.ConnectionInfo;

var builder = WebApplication.CreateBuilder(args);
var applicationDbContextConnectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
var databaseConnectionString = builder.Configuration.GetConnectionString("DataBase");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(provider =>
{
    return new ConnectionInfo(applicationDbContextConnectionString, databaseConnectionString);
});
builder.Services.AddHostedService(provider => new InitialSQLService(applicationDbContextConnectionString, databaseConnectionString,
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
app.ConfigureExceptionHandler(app.Logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
