using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Models.Dtos
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public ReportStatus Status { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
