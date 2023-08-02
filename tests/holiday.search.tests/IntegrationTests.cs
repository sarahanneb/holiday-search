using holiday.search.Clients;
using holiday.search.Models;
using holiday.search.Services;

namespace holiday.search.tests
{
    public class IntegrationTests
    {
        private readonly BookingService _sut;

        public IntegrationTests() 
        {
            _sut = new BookingService(
                new FlightDataService(new FlightDataClient()),
                new HotelDataService(new HotelDataClient())
                );
        }

        [Fact]
        public void GetBookingInformation_ShouldReturn_Flight2Hotel9()
        {
            // Arrange
            var request = new BookingRequestModel
            {
                DepartingFrom = "MAN",
                TravellingTo = "AGP",
                DepartureDate = "2023-07-01",
                Duration = 7
            };

            // Act
            var response = _sut.GetBookingInformation(request);

            // Assert
            Assert.Equal(2, response[0].FlightId);
            Assert.Equal(9, response[0].HotelId);
        }
    }
}
