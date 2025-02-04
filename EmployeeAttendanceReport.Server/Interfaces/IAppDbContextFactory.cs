using EmployeeAttendanceReport.Server.Common;

namespace EmployeeAttendanceReport.Server.Interfaces
{
    public interface IAppDbContextFactory
    {
        AppDbContext CreateDbContext(string databaseType);
    }
}
