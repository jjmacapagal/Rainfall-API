using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;

namespace Rainfall_API.Services
{
    public class RainfallSvc : IRainfallSvc
    {
        private readonly IRainfallRepository _rainfallRepo;

        public RainfallSvc(IRainfallRepository rainfallRepo)
        {
            _rainfallRepo = rainfallRepo;
        }

        // Implement the methods from IRainfallSvc here
        public async Task<List<RainfallReading>> GetReadings(string stationId, int? count)
        {
            return await GetReadings(stationId, count ?? 10);
        }

        private async Task<List<RainfallReading>> GetReadings(string stationId, int count)
        {
            return await _rainfallRepo.GetRainfallByStationId(stationId, count);
        }
    }
}