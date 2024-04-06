namespace Rainfall_API.Controllers
{
    public interface IRainfallSvc
    {
        Task<string> GetReadings(int stationId);
    }
}
