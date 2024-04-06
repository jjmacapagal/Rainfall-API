using Rainfall_API.Models.API;

namespace Rainfall_API.Interfaces
{
    public interface IRainfallSvc
    {
        Task<List<RainfallReading>> GetReadings(string stationId, int count);
    }
}