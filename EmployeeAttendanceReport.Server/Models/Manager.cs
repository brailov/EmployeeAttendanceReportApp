namespace EmployeeAttendanceReport.Server.Models
{
    public class Manager : Person
    {     
        public List<Person> Employees { get; set; } = new List<Person>();
        public List<Person> SubManagers { get; set; } = new List<Person>();
    }
}
