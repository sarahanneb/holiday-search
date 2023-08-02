using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using holiday.search.Services;
using System.Linq.Expressions;

namespace holiday.search.tests.Services
{
    public class HotelDataServiceTests
    {
        private readonly Mock<IHotelDataClient> _dataClientMock = new();

        private readonly List<HotelDataModel> TestData = new();

        private readonly HotelDataService _sut;

        public HotelDataServiceTests()
        {
            SetupTestData();

            _dataClientMock.Setup(x => x.Get()).Returns(TestData);

            _sut = new HotelDataService(_dataClientMock.Object);
        }

        [Fact]
        public void Get_WithoutFilterParamater_ShouldReturnAllData()
        {
            // Act
            var result = _sut.Get();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(TestData.Count, result.Count);

            Assert.NotNull(result.Where(x => x.Id == 1).FirstOrDefault());
            Assert.NotNull(result.Where(x => x.Id == 2).FirstOrDefault());
        }

        [Fact]
        public void Get_WithFilterParamater_ShouldReturnFilteredData()
        {
            // Arrange
            Expression<Func<HotelDataModel, bool>> filter = x => x.Name == "TestHotel1";

            // Act
            var result = _sut.Get(filter);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);

            Assert.NotNull(result.Where(x => x.Id == 1).FirstOrDefault());
            Assert.Null(result.Where(x => x.Id == 2).FirstOrDefault());
        }

        [Fact]
        public void Get_WithComplexFilterParamater_ShouldReturnFilteredData()
        {
            // Arrange
            Expression<Func<HotelDataModel, bool>> filter = x => x.Name == "TestHotel1" || x.LocalAirports.Contains("A");

            // Act
            var result = _sut.Get(filter);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);

            Assert.NotNull(result.Where(x => x.Id == 1).FirstOrDefault());
            Assert.Null(result.Where(x => x.Id == 2).FirstOrDefault());
        }

        private void SetupTestData()
        {
            TestData.Add(new HotelDataModel
            {
                Id = 1,
                Name = "TestHotel1",
                LocalAirports = new List<string> { "A" },
                PricePerNight = 100,
                ArrivalDate = "2023/02/08"
            });

            TestData.Add(new HotelDataModel
            {
                Id = 2,
                Name = "TestHotel2",
                LocalAirports = new List<string> { "B" },
                PricePerNight = 200,
                ArrivalDate = "2023/02/10"
            });
        }
    }
}
