using Microsoft.AspNetCore.Mvc;
using RobotsAPI.ResponseProcessor;
using System.Linq;

namespace RobotsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotMovementController : ControllerBase
    {
        private RobotsFetcher _fetcher = null;
        private MovementProcessor _processor = null;
        public RobotMovementController(RobotsFetcher fetcher, MovementProcessor processor)
        {
            this._fetcher = fetcher;
            this._processor = processor;
        }

        [Route("fetch")]
        [HttpPost]
        public IActionResult FetchLoad(RequestPayLoad payLoad)
        {
            if(payLoad == null || string.IsNullOrWhiteSpace(payLoad.loadId))
            {
                return BadRequest("Request is not valid");
            }

            // Call External API to fetch robots. 
            var resultItem = this._fetcher.GetRobots(payLoad.x, payLoad.y);

            if(resultItem != null && resultItem.Any())
            {
               var result = this._processor.CalculateDistanceAndMaxBatteryLife(resultItem.ToList(), payLoad.x, payLoad.y);

                    // Call Processor to get a robot by min distance less than 10 and maximum batterylife
                if (result != null)
                {
                    return Ok(result);
                }
            }

            return NotFound();
        }
    }
}
