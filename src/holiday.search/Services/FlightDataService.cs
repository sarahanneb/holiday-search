using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using holiday.search.Services.Interfaces;
using System.Linq.Expressions;

namespace holiday.search.Services
{
    public class FlightDataService : IFlightDataService
    {
        private readonly IFlightDataClient _dataClient;

        public FlightDataService(IFlightDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public List<FlightDataModel> Get()
        {
            return _dataClient.Get();
        }

        public List<FlightDataModel> Get(Expression<Func<FlightDataModel, bool>> filter)
        {
            var results = Get().AsQueryable();
            return results.Where(filter).ToList();
        }
    }
}
