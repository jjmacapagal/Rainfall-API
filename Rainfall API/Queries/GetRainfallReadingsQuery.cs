using MediatR;
using Rainfall_API.Models.API;

namespace Rainfall_API.Queries
{
    public class GetRainfallReadingsQuery : IRequest<List<RainfallReading>>
    {
        public string StationId { get; set; }

        public int Count { get; set; }

        public GetRainfallReadingsQuery(string stationId)
        {
            StationId = stationId;
            Count = 10;
        }

        public GetRainfallReadingsQuery(string stationId, int? count) : this(stationId)
        {
            Count = count ?? Count;
        }
    }
}