using holiday.search.Models;
using holiday.search.Services.Interfaces;

namespace holiday.search.Services
{
    public class BookingService : IBookingService
    {
        private readonly IFlightDataService _flightDataService;
        private readonly IHotelDataService _hotelDataService;

        public BookingService(IFlightDataService flightDataService, IHotelDataService hotelDataService)
        {
            _flightDataService = flightDataService;
            _hotelDataService = hotelDataService;
        }

        public List<BookingResponseModel> GetBookingInformation(BookingRequestModel request)
        {
            return new List<BookingResponseModel>();
        }
    }
}
