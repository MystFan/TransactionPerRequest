using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TransactionPerRequest.Data;

namespace TransactionPerRequest.Api.Extensions
{

    #nullable disable

    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase(this IServiceCollection services,
            string connectionString,
            IsolationLevel level = IsolationLevel.ReadUncommitted)
        {
            services.AddScoped<IDbConnection>((serviceProvider) =>
            {
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IDbTransaction>((serviceProvider) =>
            {
                IDbConnection dbConnection = serviceProvider.GetService<IDbConnection>();
                dbConnection.Open();

                return dbConnection.BeginTransaction(level);
            });

            services.AddScoped<DbContextOptions<ApplicationDbContext>>((serviceProvider) =>
            {
                IDbConnection connection = serviceProvider.GetRequiredService<IDbConnection>();
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer((DbConnection)connection)
                    .Options;

                return options;
            });

            services.AddScoped<ApplicationDbContext>((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
                IDbTransaction dbTransaction = serviceProvider.GetService<IDbTransaction>();

                var context = new ApplicationDbContext(options);
                context.Database.UseTransaction((DbTransaction)dbTransaction);
                return context;
            });
        }
    }
}