using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Persistence;
using PlanYourTravel.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_CONNECTIONSTRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();

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
