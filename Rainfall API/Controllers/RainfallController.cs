
using Microsoft.AspNetCore.Mvc;

namespace Rainfall_API.Controllers
{
    [ApiController]
    //
    [Route("api/[controller]")]
    public class RainfallController : ControllerBase
    {
        // 
        private readonly IRainfallSvc _rainfallService;

        public RainfallController(IRainfallSvc rainfallService)
        {
            _rainfallService = rainfallService;
        }

        [HttpGet("id/{stationId}/readings")]
        public IActionResult GetReadings(int stationId)
        {
            // Call your service to retrieve readings for the specified stationId
            var readings = _rainfallService.GetReadings(stationId);

            if (readings == null)
            {
                // Return 404 Not Found if no readings are found for the specified stationId
                return NotFound();
            }

            // Return the readings as JSON
            return Ok(readings);
        }
    }
}
