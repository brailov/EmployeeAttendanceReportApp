using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendanceReport.Server.Models.Dtos
{
    public class SubmitReportDto
    {
        [Required]
        public string Date { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
    }
}
