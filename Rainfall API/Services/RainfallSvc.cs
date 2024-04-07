using MediatR;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Queries;

namespace Rainfall_API.Services
{
    public class RainfallSvc : IRainfallSvc
    {
        private readonly IMediator _mediator;

        public RainfallSvc(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Implement the methods from IRainfallSvc here
        public async Task<List<RainfallReading>> GetReadings(string stationId, int? count)
        {
            var query = new GetRainfallReadingsQuery(stationId, count);
            var result = await _mediator.Send(query);
            return result;
        }
    }
}