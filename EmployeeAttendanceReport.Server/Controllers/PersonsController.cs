using System.Globalization;
using EmployeeAttendanceReport.Server.Common;
using EmployeeAttendanceReport.Server.Models.Dtos;
using EmployeeAttendanceReport.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IRepository _repository;

        public PersonsController(IRepository repository)
        {
            _repository = repository;
        }

        //Get employee details
        [HttpGet("{id}/employee")]
        public IActionResult GetEmployee(int id)
        {            
            var employee = _repository.GetEmployee(id);
            return employee != null ? Ok(employee) : NotFound("Person is not found");
        }

        //Get employee details
        [HttpGet("{id}/manager")]
        public IActionResult GetManager(int id)
        {
            // check that id is not employee
            var employee = _repository.GetEmployee(id);
            if (employee is not null)
            {
                return Ok(null);
            }

            // check that reporting employee exists.
            var manager = _repository.GetManager(id);
            return manager != null ? Ok(manager) : NotFound("Person is not found");
        }

        // Get list of persons in org.
        [HttpGet("all")]
        public IActionResult GetAllPersons()
        {
            var persons = _repository.GetAllPersons();          
            return persons != null ? Ok(persons) : NotFound();
        }

        //Get all manager reports
        [HttpGet("{id}/reports")]
        public IActionResult GetPersonReport(int id)
        {
            // check that reporting employee exists.
            var manager = _repository.GetManager(id);
            if (manager is null)
            {
                return NotFound("Person is not found");
            }          
            var personsReports = _repository.GetReportsByManagerId(id);
            return personsReports != null ? Ok(personsReports) : NotFound("Person has no reports");
        }

        // Create employee timesheet report
        [HttpPost("{id}/report")]
        public async Task<IActionResult> CreateReport(int id, SubmitReportDto report)
        {           
            var _endTime = Utils.StringToTimeSpan(report.EndTime);
            var _startTime = Utils.StringToTimeSpan(report.StartTime);
            string format = "dd/MM/yyyy";          
            DateTime _date = DateTime.ParseExact(report.Date, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            if (_endTime is null || _startTime is null)
                return BadRequest("Invalid credentials");

            // check that reporting employee exists
            var employee = _repository.GetEmployee(id);
            if (employee is null)
            {
                return NotFound();
            }
            
            if(!employee.ManagerId.HasValue) return BadRequest("Employee is missing manager id.");

            // check that manger is connected to employee
            var manager = _repository.GetManager(employee.ManagerId.Value);
            if (manager is null)
            {
                return NotFound();
            }
            if (!manager.Employees.Any(e => e.Id == id))
            {
                return BadRequest("Manger is not connected to employee");
            }

            // filter row for current user
            var personReports = _repository.GetActiveReportsByManagerId(manager.Id);
            if(personReports.Any(r =>  r.Date.Date == _date && r.PersonId == id 
               && ((_endTime >= r.StartTime && _endTime <= r.EndTime) ||
                  (_startTime <= r.EndTime && _startTime >= r.StartTime) ||
                  (_startTime < r.StartTime && _endTime > r.EndTime))
             ))
                return Conflict("time overlap with existing report");

            var employeeReport = new Models.PersonReport
            {
                PersonId = id,
                Id = new Guid(),
                Date = DateTime.Now,
                EndTime = _endTime.Value,
                StartTime = _startTime.Value,
                Status = ReportStatus.Pending
            };

            var res = await _repository.CreateReport(employeeReport);
            return Ok(res);
        }
    }
}
