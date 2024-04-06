using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rainfall_API.Controllers;
using Rainfall_API.Models.API;

#pragma warning disable CS8604 // Possible null reference argument.
namespace Rainfall_API.Services
{
    public class RainfallSvc : IRainfallSvc
    {
        private readonly ILogger<RainfallSvc> _logger;

        private readonly IHttpClientFactory _clientFactory;

        private readonly string _sourceRootUrl = "https://environment.data.gov.uk/flood-monitoring";

        /// <summary>
        /// Service that retrieves rainfall data from https://environment.data.gov.uk/flood-monitoring/doc/rainfall#api-summary
        /// </summary>
        public RainfallSvc(ILoggerFactory loggerFactory, IHttpClientFactory clientFactory)
        {
            _logger = loggerFactory.CreateLogger<RainfallSvc>();
            _clientFactory = clientFactory;
        }

        // Implement the methods from IRainfallSvc here
        public async Task<List<RainfallReading>> GetReadings(string stationId, int count = 10)
        {
            var client = _clientFactory.CreateClient();
            // https://environment.data.gov.uk/flood-monitoring/id/stations?parameter=rainfall&_limit=50
            //https://environment.data.gov.uk/flood-monitoring/id/stations/3680/readings?_sorted&_limit=100
            var url = $"{_sourceRootUrl}/id/stations/{stationId}/readings?_limit={count}";
            var response = await client.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var result = new List<RainfallReading>();
                var content = await response.Content.ReadAsStringAsync();
                var contentJObject = JsonConvert.DeserializeObject<JObject>(content); // Deserialize as JObject
                
                if (contentJObject == null) return result;

                // Check if "items" property exists in the JObject
                if (contentJObject.TryGetValue("items", out var itemsToken) && itemsToken is JArray itemsArray)
                {
                    foreach (var item in itemsArray)
                    {
                        var dateTime = item["dateTime"].Value<DateTime>();
                        var value = item["value"].Value<double>();
                        var reading = new RainfallReading(dateTime, value);
                        result.Add(reading);
                    }
                }
                return result;
            }

            throw new Exception("Failed to retrieve readings");
        }
    }
}