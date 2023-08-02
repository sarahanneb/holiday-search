using holiday.search.Models;
using holiday.search.Services.Interfaces;
using System.Linq.Expressions;

namespace holiday.search.Services
{
    public class BookingService : IBookingService
    {
        private readonly IFlightDataService _flightDataService;
        private readonly IHotelDataService _hotelDataService;

        private readonly List<string> LondonAirportChoices = new() { "LTN", "LGW" };

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
            var filter = CreateFlightFilter(request);

            var filteredFlights = _flightDataService.Get(filter);

            return filteredFlights == null || !filteredFlights.Any() 
                ? new List<FlightDataModel>() 
                : filteredFlights.OrderBy(x => x.Price).ToList();
        }        

        private List<HotelDataModel> GetHotelData(BookingRequestModel request)
        {
            Expression<Func<HotelDataModel, bool>> filter = x => 
                x.Nights == request.Duration
                && x.ArrivalDate == request.DepartureDate
                && x.LocalAirports.Contains(request.TravellingTo);

            var filteredHotels = _hotelDataService.Get(filter);

            return filteredHotels == null || !filteredHotels.Any() 
                ? new List<HotelDataModel>() 
                : filteredHotels.OrderBy(x => x.PricePerNight).ToList();
        }

        private Expression<Func<FlightDataModel, bool>> CreateFlightFilter(BookingRequestModel request)
        {
            Expression<Func<FlightDataModel, bool>> filter = x =>
                x.DepartureDate == request.DepartureDate
                && x.To == request.TravellingTo;

            switch (request.DepartingFrom)
            {
                case "":
                    break;
                case "ALL":
                    break;
                case "ALL LDN":
                    var filterPrefix = filter.Compile();
                    filter = x => filterPrefix(x) && LondonAirportChoices.Contains(x.From);
                    break;
                default:
                    filterPrefix = filter.Compile();
                    filter = x => filterPrefix(x) && x.From == request.DepartingFrom;
                    break;
            }

            return filter;
        }

        private static List<BookingResponseModel> CreateResponse(List<FlightDataModel> flights, List<HotelDataModel> hotels)
        {
            var response = new List<BookingResponseModel>();

            for (var i = 0; i < flights.Count && i < hotels.Count; i++)
            {
                var flight = flights[i];
                var hotel = hotels[i];

                response.Add(new BookingResponseModel
                {
                    TotalPrice = flight.Price + (hotel.PricePerNight * hotel.Nights),
                    FlightId = flight.Id,
                    FlightPrice = flight.Price,
                    FlightDepartingFrom = flight.From,
                    FlightTravellingTo = flight.To,
                    HotelId = hotel.Id,
                    HotelName = hotel.Name,
                    HotelPrice = hotel.PricePerNight * hotel.Nights
                });
            }

            return response;
        }
    }
}
