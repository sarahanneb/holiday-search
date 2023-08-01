using holiday.search.Clients;
using holiday.search.Models;
using Microsoft.Extensions.Logging;

namespace holiday.search.tests.Clients
{
    public class FlightDataClientTests
    {
        private readonly Mock<ILogger<FlightDataClient>> _loggerMock = new();

        private readonly FlightDataClient _sut;

        public FlightDataClientTests()
        {
            _sut = new FlightDataClient(_loggerMock.Object);
        }

        [Fact]
        public void Get_Returns_FlightDataList_Datatype()
        {
            // Act
            var response = _sut.Get();

            // Assert
            Assert.IsType<List<FlightDataModel>>(response);
        }

    }
}
