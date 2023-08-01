using holiday.search.Clients;
using holiday.search.Models;

namespace holiday.search.tests.Clients
{
    public class FlightDataClientTests
    {
        private readonly FlightDataClient _sut;

        public FlightDataClientTests()
        {
            _sut = new FlightDataClient();
        }

        [Fact]
        public void Get_Returns_FlightDataList_Datatype()
        {
            // Act
            var response = _sut.Get();

            // Assert
            Assert.IsType<List<FlightDataModel>>(response);
        }

        [Fact]
        public void Get_Deserializes_Json_From_Asset_File()
        {
            // Act
            var response = _sut.Get();

            // Assert
            Assert.NotEmpty(response);
            Assert.NotNull(response.Where(x => x.Id == 1).FirstOrDefault());
        }

        [Fact]
        public void Get_Should_Deserialize_All_Properties()
        {
            // Act
            var response = _sut.Get();

            // Assert
            Assert.NotEmpty(response);
            Assert.Null(response.Where(x => x.Id == default).FirstOrDefault());
            Assert.Null(response.Where(x => x.Airline == string.Empty).FirstOrDefault());
            Assert.Null(response.Where(x => x.From == string.Empty).FirstOrDefault());
            Assert.Null(response.Where(x => x.To == string.Empty).FirstOrDefault());
            Assert.Null(response.Where(x => x.Price == default).FirstOrDefault());
            Assert.Null(response.Where(x => x.DepartureDate == string.Empty).FirstOrDefault());
        }
    }
}
