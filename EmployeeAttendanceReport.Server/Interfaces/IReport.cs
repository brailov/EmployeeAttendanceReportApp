namespace EmployeeAttendanceReport.Server.Interfaces
{
    public interface IReport
    {
        DateTime Date { get; set; }
        TimeSpan StartTime { get; set; }
        TimeSpan EndTime { get; set; }
    }
}
