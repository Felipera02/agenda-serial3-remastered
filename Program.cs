
using AgendaSerial3.Infrastructure.Data;
using AgendaSerial3.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("AgendaDbStr");
if (connectionString is null)
{
    throw new Exception("The connection string was not defined in appsettings");
}

builder.Services.AddDbContext<AgendaContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddUseCases();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
