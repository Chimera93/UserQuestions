using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, string environment)
    {
        if (environment == "Test")
        {
            InMemoryDatabaseRoot _root = new InMemoryDatabaseRoot();
            services.AddEntityFrameworkInMemoryDatabase();

            services.AddDbContext<UserContext>(
                (sp, options) => options.UseInMemoryDatabase("TestingDB", _root).UseInternalServiceProvider(sp)
            );

            services.AddDbContext<QuestionContext>(
                (sp, options) => options.UseInMemoryDatabase("TestingDB", _root).UseInternalServiceProvider(sp)
            );

            services.AddTransient<TestDBInitializer>();
        }
        else
        {
            var connectionString = configuration.GetConnectionString("UserQuestions");

            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<UserContext>((sp, options) => options.UseSqlServer(connectionString).UseInternalServiceProvider(sp));

            services.AddDbContext<QuestionContext>((sp, options) => options.UseSqlServer(connectionString).UseInternalServiceProvider(sp));

        }

        return services;
    }
}