using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Services;
using PlanYourTravel.Domain.Shared.Settings;
using PlanYourTravel.Infrastructure.Persistence;
using PlanYourTravel.Infrastructure.Repositories;
using PlanYourTravel.Infrastructure.Services.TokenGenerator;
using PlanYourTravel.WebApi.Helper;

var builder = WebApplication.CreateBuilder(args);

// Check if seed argument is provided
bool shouldSeed = args.Contains("seed");

// Environment Variables
var jwtSecret = Environment.GetEnvironmentVariable(EnvironmentKey.jwtSecret);
var connectionString = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_CONNECTIONSTRING");

// Configure Services
ConfigureServices(builder.Services, builder.Configuration, jwtSecret!, connectionString!);

// Build the App
var app = builder.Build();

// Apply Migrations and Seed Data if needed
await ApplyMigrationsAndSeedData(app, shouldSeed);

// Configure Middleware and Start the App
ConfigureMiddleware(app);

app.Run();


// === Methods for Configuration ===

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration, string jwtSecret, string connectionString)
{
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

    // Domain Services and Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
    services.AddScoped<IFlightRepository, FlightRepository>();

    // Scoped Repository Factory
    services.AddTransient<Func<IFlightRepository>>(provider =>
    {
        var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        return () => scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IFlightRepository>();
    });
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