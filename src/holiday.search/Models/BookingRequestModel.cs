namespace holiday.search.Models
{
    public class BookingRequestModel
    {
        public string DepartingFrom { get; set; } = string.Empty;
        public string TravellingTo { get; set; } = string.Empty;
        public string DepartureDate { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}
