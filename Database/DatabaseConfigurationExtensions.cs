using Microsoft.EntityFrameworkCore;

namespace learn_k8s_apiserver_net.Database
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config, bool isLocalDevelopment)
        {
            var connString = config.GetConnectionString("SQL_CONNECTIONSTRING");
            var serverVersion = new MySqlServerVersion(new Version(8, 4, 4));
            services.AddDbContext<PollingApiDbContext>(options =>
            {
                options.UseMySql(connString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            return services;
        }

        public static void CreateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PollingApiDbContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
