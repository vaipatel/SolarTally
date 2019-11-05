/// Taken from <a href="https://github.com/JasonGT/NorthwindTraders/blob/master/Src/Persistence/DesignTimeDbContextFactoryBase.cs">
/// Jason Taylor's impl</a>

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace SolarTally.Persistence
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "SolarTallyDb";
        private const string PostgresUsernameKey = "PostgresUsername";
        private const string PostgresPasswordKey = "PostgresPassword";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            // Get the current dir, backup one with .. and get web app dir.
            var basePath = Directory.GetCurrentDirectory() + 
                string.Format("{0}..{0}SolarTally.WebUI_Ng",
                    Path.DirectorySeparatorChar);
            return Create(basePath,
                Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        protected abstract TContext CreateNewInstance(
            DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {
            
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Console.WriteLine("Checking if environment is development");
            // If dev env, add user secrets
            if (this.IsDevelopment(environmentName))
            {
                configBuilder.AddUserSecrets<SolarTallyDbContext>();
                Console.WriteLine($@"Environment '{environmentName}' is a development environment. User secrets added.");
            }
            else
            {
                Console.WriteLine($@"Environment '{environmentName}' is not a development environment. User secrets will not be added.");
            }
            var configuration = configBuilder.Build();

            var connStrBuilder = new NpgsqlConnectionStringBuilder(
                configuration.GetConnectionString(ConnectionStringName)
            );
            if (this.IsDevelopment(environmentName))
            {
                connStrBuilder.Username = configuration[PostgresUsernameKey];
                connStrBuilder.Password = configuration[PostgresPasswordKey];
            }
            Console.WriteLine("Using username: {0}", connStrBuilder.Username);

            return Create(connStrBuilder.ConnectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($@"Connection string '
                    {ConnectionStringName}' is null or empty.",
                    nameof(connectionString));
            }

            Console.WriteLine($@"DesignTimeDbContextFactoryBase.Create(string): 
                Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            // Using Postgres instead of MSSQL
            optionsBuilder.UseNpgsql(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }

        /// <summary>
        /// Checks that environmentName corresponds to a development env.
        /// </summary>
        /// <remarks>
        /// Borrowed from https://github.com/aspnet/AspNetCore/blob/4e44025a52e4b73aa17e09a8041b0e166e0c5ce0/src/Hosting/Abstractions/src/HostingEnvironmentExtensions.cs#L65-L78
        /// </remarks>
        private bool IsDevelopment(string environmentName)
        {
            if (environmentName == null)
            {
                throw new ArgumentNullException(nameof(environmentName));
            }

            return string.Equals(environmentName, Environments.Development,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}