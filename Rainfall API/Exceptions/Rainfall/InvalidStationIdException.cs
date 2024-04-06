namespace Rainfall_API.Exceptions.Rainfall
{
    public class InvalidStationIdException : Exception
    {
        public InvalidStationIdException() : base("No readings found for the specified stationId.")
        {
        }

        public InvalidStationIdException(string message) : base(message)
        {
        }

        public InvalidStationIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
