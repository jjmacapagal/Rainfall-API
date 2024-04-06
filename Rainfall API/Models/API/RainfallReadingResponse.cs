using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall_API.Models.API
{
    [SwaggerSchema(Title = "Rainfall reading response", Description = "Details of a rainfall reading")]
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; }

        public RainfallReadingResponse()
        {
            Readings = new List<RainfallReading>();
        }

        public RainfallReadingResponse(List<RainfallReading> readings) : this()
        {
            Readings = readings;
        }
    }
}