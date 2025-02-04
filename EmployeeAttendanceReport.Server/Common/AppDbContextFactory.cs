using EmployeeAttendanceReport.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceReport.Server.Common
{
    public class AppDbContextFactory : IAppDbContextFactory
    {        
        private readonly IConfiguration _configuration;

        public AppDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppDbContext CreateDbContext(string databaseType)
        {
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();

            if (databaseType == "SQLServer")
                optionBuilder.UseSqlServer(_configuration.GetConnectionString("SQLServer"));
            else if (databaseType == "Oracle")
                optionBuilder.UseOracle(_configuration.GetConnectionString("Oracle"));
            else throw new ArgumentException("Invalid  database type specified");

            return new AppDbContext(optionBuilder.Options);
        }
    }
}
