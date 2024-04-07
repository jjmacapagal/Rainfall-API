using Rainfall_API.Models.API;

namespace Rainfall_API.Interfaces
{
    public interface IRainfallRepository
    {
        public Task<List<RainfallReading>> GetRainfallByStationId(string Id, int count);
    }
}