using holiday.search.Models;
using holiday.search.Services;
using holiday.search.Services.Interfaces;
using System.Linq.Expressions;

namespace holiday.search.tests.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IFlightDataService> _flightDataServiceMock = new();
        private readonly Mock<IHotelDataService> _hotelDataServiceMock = new();

        private readonly List<FlightDataModel> TestFlightData = new();
        private readonly List<HotelDataModel> TestHotelData = new();

        private readonly BookingService _sut;

        public BookingServiceTests()
        {
            SetupTestData();

            _sut = new BookingService(_flightDataServiceMock.Object, _hotelDataServiceMock.Object);
        }

        [Fact]
        public void GetBookingInformation_ShouldReturn_BookingResponseModelListType()
        {
            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            Assert.IsType<List<BookingResponseModel>>(response);
        }

        [Fact]
        public void GetBookingInformation_ShouldCall_FlightAndHotelServices()
        {
            // Act
            _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            _flightDataServiceMock.Verify(x => x.Get(It.IsAny<Expression<Func<FlightDataModel, bool>>>()), Times.Once);
            _hotelDataServiceMock.Verify(x => x.Get(It.IsAny<Expression<Func<HotelDataModel, bool>>>()), Times.Once);
        }

        [Fact]
        public void GetBookingInformation_ShouldReturnEmptyResponseList_IfNoFlightsOrHotelsAvailable()
        {
            // Arrange
            _flightDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<FlightDataModel, bool>>>())).Returns(new List<FlightDataModel>());
            _hotelDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<HotelDataModel, bool>>>())).Returns(new List<HotelDataModel>());

            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void GetBookingInformation_ShouldOrderFlightsAndHotels_ByPrice()
        {
            // Arrange
            _flightDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<FlightDataModel, bool>>>())).Returns(TestFlightData);
            _hotelDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<HotelDataModel, bool>>>())).Returns(TestHotelData);

            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(1, response.First().FlightId);
            Assert.Equal(1, response.First().HotelId);
        }

        [Fact]
        public void GetBookingInformation_ShouldCalculateHotelPrice_UsingPricePerNightAndNights()
        {
            // Arrange
            _flightDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<FlightDataModel, bool>>>())).Returns(TestFlightData);
            _hotelDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<HotelDataModel, bool>>>())).Returns(TestHotelData);

            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(1000, response.First().HotelPrice);
        }

        [Fact]
        public void GetBookingInformation_ShouldCalculateTotalPrice_UsingFlightPriceAndHotelPrice()
        {
            // Arrange
            _flightDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<FlightDataModel, bool>>>())).Returns(TestFlightData);
            _hotelDataServiceMock.Setup(x => x.Get(It.IsAny<Expression<Func<HotelDataModel, bool>>>())).Returns(TestHotelData);

            // Act
            var response = _sut.GetBookingInformation(new BookingRequestModel());

            // Assert
            Assert.NotEmpty(response);
            Assert.Equal(1100, response.First().TotalPrice);
        }

        private void SetupTestData()
        {
            TestFlightData.Add(new FlightDataModel
            {
                Id = 1,
                Airline = "TestAirline1",
                From = "A",
                To = "B",
                Price = 100,
                DepartureDate = "2023/02/08"
            });

            TestFlightData.Add(new FlightDataModel
            {
                Id = 2,
                Airline = "TestAirline2",
                From = "X",
                To = "Y",
                Price = 200,
                DepartureDate = "2023/02/10"
            });

            TestHotelData.Add(new HotelDataModel
            {
                Id = 1,
                Name = "TestHotel1",
                LocalAirports = new List<string> { "B" },
                PricePerNight = 100,
                ArrivalDate = "2023/02/08",
                Nights = 10
            });

            TestHotelData.Add(new HotelDataModel
            {
                Id = 2,
                Name = "TestHotel2",
                LocalAirports = new List<string> { "Y" },
                PricePerNight = 200,
                ArrivalDate = "2023/02/10",
                Nights = 10
            });
        }
    }
}
