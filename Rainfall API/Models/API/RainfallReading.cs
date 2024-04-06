namespace Rainfall_API.Models.API
{
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