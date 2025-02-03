namespace EmployeeAttendanceReport.Server.Models.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? Role { get; set; }     
        public string? Manager { get; set; } = string.Empty;
    }
}
