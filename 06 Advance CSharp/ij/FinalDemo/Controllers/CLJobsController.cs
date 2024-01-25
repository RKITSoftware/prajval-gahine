using FinalDemo.Models;
using FinalDemo.Service;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    [RoutePrefix("api/Jobs")]
    public class CLJobsController : ApiController
    {
        private static BLJobService _jobService;

        static CLJobsController()
        {
            // Initialize the job service
            _jobService = new BLJobService();
        }

        [HttpGet, Route("GetAllJobs")]
        public IHttpActionResult GetAllJobs()
        {
            var jobs = _jobService.GetJobs();
            if (jobs != null)
                return Ok(jobs);
            else
                return InternalServerError();
        }

        [HttpGet, Route("GetJob/{id}")]
        public IHttpActionResult GetJob([FromUri] int id)
        {
            var job = _jobService.GetJobById(id);
            if (job != null)
                return Ok(job);
            else
                return NotFound();
        }

        [HttpPost, Route("AddJob")]
        public IHttpActionResult AddJob([FromBody] Job01 job)
        {
            var result = _jobService.AddJob(job);
            if (result != null)
                return Created(Request.RequestUri + "/" + result.b01f01, result);
            else
                return InternalServerError();
        }

        [HttpPut, Route("EditJob/{id}")]
        public IHttpActionResult EditJob(int id, [FromBody] Job01 job)
        {
            var existingJob = _jobService.GetJobById(id);
            if (existingJob == null)
                return NotFound();

            var result = _jobService.EditJob(id, job);
            if (result != null)
                return Ok(result);
            else
                return InternalServerError();
        }

        [HttpGet, Route("DeleteJob/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var success = _jobService.DeleteJob(id);
            if (success)
                return Ok($"Job with ID = {id} has been deleted");
            else
                return NotFound();
        }
    }
}