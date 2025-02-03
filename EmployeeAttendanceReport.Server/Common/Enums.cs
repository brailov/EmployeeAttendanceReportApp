namespace EmployeeAttendanceReport.Server.Common
{
    public class Enums
    {
        public enum ReportStatus
        {           
            Pending = 0,
            Approved = 1,
            Reject = 2
        }

        public enum Role
        {
            None = 0,
            Nurse = 1,
            Doctor = 2,
            Secretary = 3
        }
    }
}
