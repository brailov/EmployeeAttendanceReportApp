using EmployeeAttendanceReport.Server.Models;
using EmployeeAttendanceReport.Server.Models.Dtos;
using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Repositories
{
    public interface IRepository {      
        public Employee? GetEmployee(int id);
        public ManagerDto? GetManager(int id);
        public List<PersonDto> GetAllPersons();
        public List<ReportDto> GetReportsByManagerId(int id, ReportStatus status = ReportStatus.Pending);
        public List<ReportDto> GetActiveReportsByManagerId(int id);
        public Task<PersonReport> CreateReport(PersonReport report);
        public Task<bool> UpdateReport(Guid reportId, ReportStatus status);
    }
    
}
