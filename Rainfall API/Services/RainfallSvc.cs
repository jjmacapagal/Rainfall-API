using Rainfall_API.Controllers;

namespace Rainfall_API.Services
{
    public class RainfallSvc : IRainfallSvc
    {
        // Implement the methods from IRainfallSvc here
        public Task<string> GetReadings(int stationId, int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}