using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PlanYourTravel.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_CONNECTIONSTRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "The connection string environment variable was not found.");
            }

            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
