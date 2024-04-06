using Swashbuckle.AspNetCore.Annotations;

namespace Rainfall_API.Models.API
{
    [SwaggerSchema(Title = "Rainfall reading", Description = "Details of a rainfall reading")]    
    public class RainfallReading
    {
        public DateTime DateMeasured { get; set; }
        
        public double AmountMeasured { get; set; }

        public RainfallReading(DateTime dateMeasured, double amountMeasured)
        {
            DateMeasured = dateMeasured;
            AmountMeasured = amountMeasured;
        }
    }
}