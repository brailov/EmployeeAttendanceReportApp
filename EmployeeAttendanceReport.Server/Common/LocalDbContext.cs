using EmployeeAttendanceReport.Server.Models;
using Microsoft.EntityFrameworkCore;
using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Common
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
     
        public DbSet<PersonReport> Reports { get; set; }
      

        //Seed data - This method is called when EF Core builds the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            var manager1 = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
            var manager2 = new Person { Id = 2, FirstName = "Dave", LastName = "Smith" };
            var employee1 = new Person { Id = 3, FirstName = "Kim", LastName = "Yang", Role = (int)Role.Nurse, ManagerId = 1 };
            var employee2 = new Person { Id = 4, FirstName = "Samantha", LastName = "White", Role = (int)Role.Secretary, ManagerId = 1 };
            var employee3 = new Person { Id = 5, FirstName = "Alex", LastName = "Williams", Role = (int)Role.Doctor, ManagerId = 2 };

            modelBuilder.Entity<Person>().HasData(manager1, manager2, employee1, employee2, employee3);
            
        }
    }
}
