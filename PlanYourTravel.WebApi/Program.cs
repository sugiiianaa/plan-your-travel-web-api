using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;
using PlanYourTravel.Infrastructure.Repositories;
using PlanYourTravel.Infrastructure.Services.GetCurrentUser;
using PlanYourTravel.Infrastructure.Services.PasswordHasher;
using PlanYourTravel.Infrastructure.Services.TokenGenerator;
using PlanYourTravel.Shared.AppSettings;
using PlanYourTravel.WebApi.Helper;

// TODO : host it on railway
var builder = WebApplication.CreateBuilder(args);

// Check if seed argument is provided
bool shouldSeed = args.Contains("seed");

// Environment Variables
var jwtSecret = Environment.GetEnvironmentVariable(EnvironmentVariableKey.jwtSecret);
var connectionString = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_CONNECTIONSTRING");

// Configure Services
ConfigureServices(builder.Services, builder.Configuration, jwtSecret!, connectionString!);

// Build the App
var app = builder.Build();

// Apply Migrations and Seed Data if needed
await ApplyMigrationsAndSeedData(app, shouldSeed);

// Configure Middleware and Start the App
ConfigureMiddleware(app);

// Using hangfire dashboard
app.UseHangfireDashboard("/hangfire");

app.Run();


// === Methods for Configuration ===

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration, string jwtSecret, string connectionString)
{
    // Hangfire service
    services.AddHangfire(config =>
    {
        config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(
                bootstrapperOptions =>
                {
                    bootstrapperOptions.UseNpgsqlConnection(connectionString);
                },
                new PostgreSqlStorageOptions
                {
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    InvisibilityTimeout = TimeSpan.FromMinutes(5),
                }
            );
    });

    services.AddHangfireServer();

    // Configure Jwt Settings
    services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

    // Authentication
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });

    // Authorization Policies
    services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
        options.AddPolicy("SuperUserPolicy", policy => policy.RequireRole("SuperUser"));
    });

    // Database Context
    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());

    // Controllers, Swagger, and MediatR
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly);
    });

    // HttpContext Accessor 
    services.AddHttpContextAccessor();

    // Domain Services and Repositories
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IGetCurrentUser, GetCurrentUser>();

    services.AddScoped<IAirportRepository, AirportRepository>();
    services.AddScoped<IFlightScheduleRepository, FlightScheduleRepository>();
    services.AddScoped<IFlightSeatClassRepository, FlightSeatClassRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ILocationRepository, LocationRepository>();
    services.AddScoped<IFlightTransactionRepository, FlightTransactionRepository>();
}

async Task ApplyMigrationsAndSeedData(WebApplication app, bool shouldSeed)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        // Apply pending migrations
        await dbContext.Database.MigrateAsync();

        // Seed database if the 'seed' argument is provided
        if (shouldSeed)
        {
            await DatabaseSeedingHelper.SeedData(dbContext);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database Migration Failed: {ex.Message}");
        throw;
    }
}

void ConfigureMiddleware(WebApplication app)
{
    // Swagger only in Development
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}