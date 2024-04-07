using MediatR;
using Rainfall_API.Models.API;

namespace Rainfall_API.Queries
{
    public class GetRainfallReadingsQuery : IRequest<List<RainfallReading>>
    {
        public string StationId { get; set; }

        public int Count { get; set; }

        public GetRainfallReadingsQuery(string stationId, int? count)
        {
            StationId = stationId;
            Count = count ?? 10;
        }
    }
}