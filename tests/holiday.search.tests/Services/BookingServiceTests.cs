using holiday.search.Models;
using holiday.search.Services;
using holiday.search.Services.Interfaces;

namespace holiday.search.tests.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IFlightDataService> _flightDataServiceMock = new();
        private readonly Mock<IHotelDataService> _hotelDataServiceMock = new();

        private readonly BookingService _sut;

        public BookingServiceTests()
        {
            _sut = new BookingService(_flightDataServiceMock.Object, _hotelDataServiceMock.Object);
        }

        [Fact]
        public void GetBookingInformation_ShouldReturn_BookingResponseModelListType()
        {
            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            Assert.IsType<List<BookingResponseModel>>(response);
        }
    }
}
