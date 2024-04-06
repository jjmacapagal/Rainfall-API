using Rainfall_API.Models.Attributes;

namespace Rainfall_API.Models.API
{
    /// <summary>
    /// Details of a rainfall reading
    /// </summary>
    [Title("Rainfall Reading Response")]
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