using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;

namespace ExpenseSplittingApplication.Controllers
{
    /// <summary>
    /// Controller for testing purposes.
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// Action method that returns a JSON response containing a name.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the JSON response.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { name = "ESA" });
        }

        //[AllowAnonymous]
        //[HttpGet("thread2")]
        //public Task<IActionResult> Index3()
        //{
        //    DateTime startTime = DateTime.Now;
        //    var tcs = new TaskCompletionSource<IActionResult>();

        //    // Using a timer for non-blocking delay
        //    var timer = new Timer(_ =>
        //    {
        //        DateTime endTime = DateTime.Now;
        //        TimeSpan diff = endTime - startTime;
        //        tcs.SetResult(Ok(new { name = "ESA", startTime = startTime, endTime = endTime, diff = diff.TotalSeconds }));
        //    }, null, 10000, Timeout.Infinite);

        //    // Returning a Task that will complete when the timer fires
        //    return new Task<IActionResult>(() => tcs.Task.Result);
        //}


        /// <summary>
        /// Action method that returns a JSON response containing a name.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the JSON response.</returns>
        [AllowAnonymous]
        [HttpGet("thread")]
        public IActionResult Index2(int id)
        {
            DateTime startTime = DateTime.Now;
            if (id == 1)
            {
                Test.UserId = id;
                Thread.Sleep(10000);
                //await Task.Delay(10000);
            }
            else if (id == 2)
            {
                Test.UserId = id;
                Thread.Sleep(20000);
                //await Task.Delay(20000);
            }
            DateTime endTime = DateTime.Now;
            TimeSpan diff = endTime - startTime;
            return Ok(new { name = "ESA", startTime = startTime, endTime = endTime, diff = diff.TotalSeconds, id = Test.UserId });
        }
    }

    public class Test
    {
        public static int UserId { get; set; }
    }
}
