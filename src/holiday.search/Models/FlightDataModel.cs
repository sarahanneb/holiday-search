using System.Text.Json.Serialization;

namespace holiday.search.Models
{
    public class FlightDataModel
    {
        public int Id { get; set; }
        public string Airline { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public int Price { get; set; }

        [JsonPropertyName("departure_date")]
        public string DepartureDate { get; set; } = string.Empty;
    }
}
