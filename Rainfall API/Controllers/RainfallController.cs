
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Rainfall_API.Exceptions.Rainfall;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Operations relating to rainfall")]
    public class RainfallController : ControllerBase
    {
        // 
        private readonly IRainfallSvc _rainfallService;

        private readonly ILogger<RainfallController> _logger;

        public RainfallController(IRainfallSvc rainfallService, ILogger<RainfallController> logger)
        {
            _rainfallService = rainfallService;
            _logger = logger;
        }

        // v1.0
        private async Task<ActionResult<RainfallReadingResponse>> GetReadingsV1(string stationId, int count)
        {
            var readings = await _rainfallService.GetReadings(stationId, count);
            if (readings.Count == 0)
            {
                return NotFound(new ErrorResponse("No readings found for the specified stationId"));
            }

            return Ok(new RainfallReadingResponse(readings));
        }

        // in case we want to use the versioning in the URL
        // public ActionResult<RainfallReadingResponse> GetReadings(string stationId, [FromQuery, Range(1, 100)] int count = 10, [FromQuery] string version = "1.0")

        /// <param name="stationId">The id of the reading station.</param>
        /// <param name="count">The number of readings to return</param>
        /// <returns>A list of rainfall readings successfully retrieved.</returns>
        [HttpGet("id/{stationId}/readings")]
        [SwaggerOperation(
            OperationId = "get-rainfall", 
            Summary = "Get rainfall readings by station Id", 
            Description = "Retrieve the latest readings for the specified stationId")
        ]
        [SwaggerResponse(200, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponse))]
        [SwaggerResponse(400, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(404, "No readings found for the specified stationId", typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
        public async Task<ActionResult<RainfallReadingResponse>> GetReadings(string stationId, [FromQuery, Range(1, 100)] int count = 10)
        {
            try
            {
                if (count < 1 || count > 100)
                {
                    return BadRequest(new ErrorResponse("Invalid count"));
                }

                return await GetReadingsV1(stationId, count);

                // // Check the version query parameter
                // switch (version)
                // {
                //     case "1.0":
                //         return GetReadingsV1(id, count);
                //     default:
                //         return BadRequest(new ErrorResponse("Invalid version"));
                // }
            }
            catch (InvalidStationIdException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(new ErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting readings");
                return StatusCode(500, new ErrorResponse("Error getting readings"));
            }
        }
    }
}
