using holiday.search.Models;
using holiday.search.Services.Interfaces;
using System.Linq.Expressions;

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
            var flights = GetFlightData(request);
            var hotels = GetHotelData(request);

            if (!flights.Any() || !hotels.Any())
            {
                return new List<BookingResponseModel>();
            }

            return CreateResponse(flights, hotels);
        }

        private List<FlightDataModel> GetFlightData(BookingRequestModel request)
        {
            Expression<Func<FlightDataModel, bool>> filter = x => 
                x.DepartureDate == request.DepartureDate
                && x.To == request.TravellingTo;

            var filteredFlights = _flightDataService.Get(filter);

            return filteredFlights == null || !filteredFlights.Any() 
                ? new List<FlightDataModel>() 
                : filteredFlights.OrderBy(x => x.Price).ToList();
        }

        private List<HotelDataModel> GetHotelData(BookingRequestModel request)
        {
            Expression<Func<HotelDataModel, bool>> filter = x => 
                x.Nights == request.Duration
                && x.LocalAirports.Contains(request.TravellingTo);

            var filteredHotels = _hotelDataService.Get(filter);

            return filteredHotels == null || !filteredHotels.Any() 
                ? new List<HotelDataModel>() 
                : filteredHotels.OrderBy(x => x.PricePerNight).ToList();
        }

        private static List<BookingResponseModel> CreateResponse(List<FlightDataModel> flights, List<HotelDataModel> hotels)
        {
            var flight = flights.First();
            var hotel = hotels.First();

            return new List<BookingResponseModel>()
            {
                new BookingResponseModel
                {
                    TotalPrice = flight.Price + (hotel.PricePerNight * hotel.Nights),
                    FlightId = flight.Id,
                    FlightPrice = flight.Price,
                    FlightDepartingFrom = flight.From,
                    FlightTravellingTo = flight.To,
                    HotelId = hotel.Id,
                    HotelName = hotel.Name,
                    HotelPrice = hotel.PricePerNight * hotel.Nights
                }
            };
        }
    }
}
