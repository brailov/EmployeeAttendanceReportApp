using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceReport.Server.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }  
    }       
}
