using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using System.Text.Json;

namespace holiday.search.Clients
{
    public class HotelDataClient : IHotelDataClient
    {
        public HotelDataClient() { }

        public List<HotelDataModel> Get()
        {
            var fileName = "./Assets/hotels.json";
            var jsonString = File.ReadAllText(fileName);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<HotelDataModel> hotelDataList = JsonSerializer.Deserialize<List<HotelDataModel>>(jsonString, options)!;

            return hotelDataList;
        }
    }
}
