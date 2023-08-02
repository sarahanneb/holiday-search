namespace holiday.search.Models
{
    public class BookingResponseModel
    {
        public int TotalPrice { get; set; }
        public int FlightId { get; set; }
        public int FlightPrice { get; set; }
        public string FlightDepartingFrom { get; set; } = string.Empty;
        public string FlightTravellingTo { get; set; } = string.Empty;
        public int HotelId { get; set; }
        public int HotelPrice { get; set; }
        public string HotelName { get; set; } = string.Empty;
    }
}
