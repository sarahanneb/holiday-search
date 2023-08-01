using holiday.search.Clients;
using holiday.search.Models;

namespace holiday.search.tests.Clients
{
    public class HotelDataClientTests
    {
        private readonly HotelDataClient _sut;

        public HotelDataClientTests()
        {
            _sut = new HotelDataClient();
        }

        [Fact]
        public void Get_Returns_HotelDataList_Datatype()
        {
            // Act
            var response = _sut.Get();

            // Assert
            Assert.IsType<List<HotelDataModel>>(response);
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
            Assert.Null(response.Where(x => x.Name == string.Empty).FirstOrDefault());
            Assert.Null(response.Where(x => x.ArrivalDate == string.Empty).FirstOrDefault());
            Assert.Null(response.Where(x => x.PricePerNight == default).FirstOrDefault());
            Assert.Null(response.Where(x => x.LocalAirports == default).FirstOrDefault());
            Assert.Null(response.Where(x => x.Nights == default).FirstOrDefault());
        }
    }
}
