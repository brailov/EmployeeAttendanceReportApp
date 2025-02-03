using EmployeeAttendanceReport.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using static EmployeeAttendanceReport.Server.Common.Enums;

namespace EmployeeAttendanceReport.Server.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : Controller
    {  
        private readonly IRepository _repository;

        public ReportsController(IRepository repository)
        {         
            _repository = repository;
        }    

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateReport(string id, [FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))          
            {         
                return BadRequest("Missing status value in query.");
            }
            if ( !Enum.TryParse<ReportStatus>(status, true, out var itemStatus))
            {
                return BadRequest("Invalid status value.");
            }

            Guid reportId;
            if (!Guid.TryParse(id, out reportId))
            {
                return BadRequest("Invalid ID format");
            }

            var res = await _repository.UpdateReport(reportId, itemStatus);            
            return Ok(res);
        }

    }
}
