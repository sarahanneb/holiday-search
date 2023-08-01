using System.Text.Json.Serialization;

namespace holiday.search.Models
{
    public class HotelDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("arrival_date")]
        public string ArrivalDate { get; set; } = string.Empty;

        [JsonPropertyName("price_per_night")]
        public int PricePerNight { get; set; }

        [JsonPropertyName("local_airports")]
        public List<string> LocalAirports { get; set; } = new();

        public int Nights { get; set; }
    }
}
