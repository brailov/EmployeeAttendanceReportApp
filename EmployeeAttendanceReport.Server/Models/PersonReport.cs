using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Models
{
    public class PersonReport : Report
    {
        public Guid Id { get; set; }
        public int PersonId { get; set; }      
        public ReportStatus Status { get; set; }
    }
}
