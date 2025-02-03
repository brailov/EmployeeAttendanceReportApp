using EmployeeAttendanceReport.Server.Interfaces;

namespace EmployeeAttendanceReport.Server.Models
{
    public class Report : IReport
    {        
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }    
    }
}
