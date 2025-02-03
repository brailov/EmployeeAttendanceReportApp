using EmployeeAttendanceReport.Server.Interfaces;

namespace EmployeeAttendanceReport.Server.Models
{
    public class Person : IPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Role { get; set; }
        public int? ManagerId { get; set; }       
    }
}
