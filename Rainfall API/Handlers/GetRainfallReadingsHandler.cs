using MediatR;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Queries;

namespace Rainfall_API.Handlers
{
    public class GetRainfallReadingsHandler : IRequestHandler<GetRainfallReadingsQuery, List<RainfallReading>>
    {
        private readonly IRainfallRepository _rainfallRepo;

        public GetRainfallReadingsHandler(IRainfallRepository rainfallRepo)
        {
            _rainfallRepo = rainfallRepo;
        }

        public async Task<List<RainfallReading>> Handle(GetRainfallReadingsQuery query, CancellationToken cancellationToken)
        {
            return await _rainfallRepo.GetRainfallByStationId(query.StationId, query.Count);
        }
    }
}