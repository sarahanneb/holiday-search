using holiday.search.Models;

namespace holiday.search.Clients.Interfaces
{
    public interface IFlightDataClient
    {
        public List<FlightDataModel> Get();
    }
}
