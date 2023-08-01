using holiday.search.Clients.Interfaces;
using holiday.search.Models;

namespace holiday.search.Clients
{
    public class FlightDataClient : IFlightDataClient
    {
        private readonly ILogger<FlightDataClient> _logger;

        public FlightDataClient(ILogger<FlightDataClient> logger)
        {
            _logger = logger;
        }

        public List<FlightDataModel> Get()
        {
            return new List<FlightDataModel>();
        }
    }
}
