
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Rainfall_API.Models.API;

namespace Rainfall_API.Controllers
{
    [ApiController]
    //
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        // 
        private readonly IRainfallSvc _rainfallService;

        public RainfallController(IRainfallSvc rainfallService)
        {
            _rainfallService = rainfallService;
        }

        // v1.0
        private ActionResult<RainfallReadingResponse> GetReadingsV1(int stationId, int count)
        {
            // Your existing logic for version 1.0
            // This is where you return List<RainfallReading>
            return Ok();
        }

        [HttpGet("id/{stationId}/readings")]
        [ProducesResponseType(typeof(RainfallReadingResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public ActionResult<RainfallReadingResponse> GetReadings(string stationId, [FromQuery, Range(1, 100)] int count = 10, [FromQuery] string version = "1.0")
        {
            if (string.IsNullOrWhiteSpace(stationId))
            {
                return BadRequest(new ErrorResponse("Invalid stationId"));
            }

            if (count < 1 || count > 100)
            {
                return BadRequest(new ErrorResponse("Invalid count"));
            }

            var id = int.Parse(stationId);

            // Check the version query parameter
            switch (version)
            {
                case "1.0":
                    return GetReadingsV1(id, count);
                default:
                    return BadRequest(new ErrorResponse("Invalid version"));
            }
        }
    }
}
