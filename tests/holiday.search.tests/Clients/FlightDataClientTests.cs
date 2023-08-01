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
            Assert.NotNull(response.Where(x => x.Id == 1));
        }
    }
}
