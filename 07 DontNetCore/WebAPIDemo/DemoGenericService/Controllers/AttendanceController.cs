using DemoGenericService.BL;
using DemoGenericService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoGenericService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public AttendanceController(BLResource<ATD01> blAttendance)
        {
            Console.WriteLine(blAttendance.GetHashCode());
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Got Attendance");
        }
    }
}
