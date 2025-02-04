using EmployeeAttendanceReport.Server.Common;
using EmployeeAttendanceReport.Server.Models;
using EmployeeAttendanceReport.Server.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Repositories
{
    public class Repository(LocalDbContext context) : IRepository
    {       

        public Employee? GetEmployee(int id) 
        {
             return (from person in context.Persons
                           let personsManager = context.Persons.Where(x => x.Id == person.ManagerId).FirstOrDefault()
                           where person.Id == id && person.Role != null
                            select new Employee
                            {
                                Id = person.Id,
                                FirstName = person.FirstName,
                                LastName = person.LastName,
                                Manager = personsManager == null ? "" : personsManager.FirstName+ " " +personsManager.LastName,
                                ManagerId = person.ManagerId,
                                Role = person.Role                                         
                            }).FirstOrDefault();
        }

        // Get manager details with all pending reports waiting for his approval.
        public ManagerDto? GetManager(int id)
        {
            return (from person in context.Persons
                          let subManagers = context.Persons.Where(x => x.ManagerId == id && (x.Role == null || x.Role == 0)).ToList()
                          let employees = context.Persons.Where(x => x.ManagerId == id && x.Role != null && x.Role > 0).ToList()
                          where person.Id == id && person.Role == null
                          select new ManagerDto
                          {
                              Id = person.Id,
                              FirstName = person.FirstName,
                              LastName = person.LastName,                             
                              ManagerId = person.ManagerId,
                              Role = person.Role,
                              Employees = employees,
                              SubManagers = subManagers

                          }).FirstOrDefault();
        }

        public List<PersonDto> GetAllPersons()
        {
            return (from person in context.Persons
                          join manager in context.Persons on person.ManagerId equals manager.Id into managers
                          from manager in managers.DefaultIfEmpty()
                          select new PersonDto
                          {
                              Id = person.Id,
                              FirstName = person.FirstName,
                              LastName = person.LastName,
                              Manager = manager == null ? "" : manager.FirstName + " " + manager.LastName,
                              Role = person.Role
                          }).ToList();
        }

        // Return all time reports that have status pending
        public List<ReportDto> GetReportsByManagerId(int id, ReportStatus status = ReportStatus.Pending)
        {                      
            return (from persons in context.Persons
                    join reports in context.Reports on persons.Id equals reports.PersonId                                        
                    where( persons.ManagerId == id && persons.Role != null && persons.Role > 0) && reports.Status == status
                    select new ReportDto
                    {
                        Id = reports.Id,
                        Status = reports.Status,
                        PersonId = reports.PersonId,
                        Name = persons.FirstName + " " + persons.LastName,
                        Date = reports.Date,
                        StartTime = reports.StartTime,
                        EndTime = reports.EndTime

                    }).ToList();        
        }

        // Return all time reports that have status pending or approved
        public List<ReportDto> GetActiveReportsByManagerId(int id)
        {
            return (from persons in context.Persons
                    join reports in context.Reports on persons.Id equals reports.PersonId
                    where (persons.ManagerId == id && persons.Role != null && persons.Role > 0) && reports.Status != ReportStatus.Reject
                    select new ReportDto
                    {
                        Id = reports.Id,
                        Status = reports.Status,
                        PersonId = reports.PersonId,
                        Name = persons.FirstName + " " + persons.LastName,
                        Date = reports.Date,
                        StartTime = reports.StartTime,
                        EndTime = reports.EndTime

                    }).ToList();
        }

        public async Task<PersonReport> CreateReport(PersonReport report)
        {
            var res = context.Reports.Add(report);
            await context.SaveChangesAsync();
            return res.Entity;
        }
             
        public async Task<bool> UpdateReport(Guid reportId, ReportStatus status)
        {
            var report = context.Reports.Find(reportId);
            if (report == null) return false;
            report.Status = status;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
