using MediatR;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Queries;

namespace Rainfall_API.Handlers
{
    public class GetRainfallReadingsHandler : IRequestHandler<GetRainfallReadingsQuery, List<RainfallReading>>
    {
        private readonly IRainfallSvc _rainfallSvc;

        public GetRainfallReadingsHandler(IRainfallSvc rainfallRepo)
        {
            _rainfallSvc = rainfallRepo;
        }

        public async Task<List<RainfallReading>> Handle(GetRainfallReadingsQuery query, CancellationToken cancellationToken)
        {
            return await _rainfallSvc.GetReadings(query.StationId, query.Count);
        }
    }
}