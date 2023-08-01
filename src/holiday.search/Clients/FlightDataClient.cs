using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using System.Text.Json;

namespace holiday.search.Clients
{
    public class FlightDataClient : IFlightDataClient
    {
        public FlightDataClient() { }

        public List<FlightDataModel> Get()
        {
            var fileName = "./Assets/flights.json";
            var jsonString = File.ReadAllText(fileName);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<FlightDataModel> flightDataList = JsonSerializer.Deserialize<List<FlightDataModel>>(jsonString, options)!;

            return flightDataList;
        }
    }
}
