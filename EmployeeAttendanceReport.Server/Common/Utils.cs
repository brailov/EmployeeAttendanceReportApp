using EmployeeAttendanceReport.Server.Models;

namespace EmployeeAttendanceReport.Server.Common
{
    public static class Utils
    {       
        public static bool HasTimeOverlap(List<PersonReport> reports, Report newReport)
        {
            return reports.Any(r =>
                r.Date == newReport.Date && // Ensure it's the same date
                (newReport.StartTime < r.EndTime && newReport.EndTime > r.StartTime) // Check for overlap
            );
        }

        public static TimeSpan? StringToTimeSpan(string timeStr)
        {
            if (string.IsNullOrEmpty(timeStr))
            {
                return null; // Or handle null/empty as needed
            }

            try
            {
                // Split the string by the semicolon
                string[] parts = timeStr.Split(':');
                if (parts.Length != 2)
                {
                    //Console.WriteLine($"Invalid time format: {timeStr}. Expected format: 'HH;mm'");
                    return null;
                }

                if (int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                {
                    if (hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60)
                    {
                        return new TimeSpan(hours, minutes, 0);
                    }
                    else
                    {
                        //Console.WriteLine($"Invalid time value: {timeStr}. Hours must be between 0 and 23, minutes between 0 and 59.");
                        return null;
                    }
                }
                else
                {
                    //Console.WriteLine($"Invalid time format: {timeStr}. Hours and minutes must be integers.");
                    return null;
                }
            }
            catch (FormatException)
            {
                //Console.WriteLine($"Invalid time format: {timeStr}. Expected format: 'HH;mm'");
                return null;
            }
        }
    }
}
