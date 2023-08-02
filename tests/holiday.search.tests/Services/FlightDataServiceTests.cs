using holiday.search.Clients.Interfaces;
using holiday.search.Models;
using holiday.search.Services;
using System.Linq.Expressions;

namespace holiday.search.tests.Services
{
    public class FlightDataServiceTests
    {
        private readonly Mock<IFlightDataClient> _dataClientMock = new();

        private readonly List<FlightDataModel> TestData = new();

        private readonly FlightDataService _sut;

        public FlightDataServiceTests()
        {
            SetupTestData();

            _dataClientMock.Setup(x => x.Get()).Returns(TestData);

            _sut = new FlightDataService(_dataClientMock.Object);    
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
            Expression<Func<FlightDataModel, bool>> filter = x => x.Airline == "TestAirline1";

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
            TestData.Add(new FlightDataModel 
            {
                Id = 1,
                Airline = "TestAirline1",
                From = "A",
                To = "B",
                Price = 100,
                DepartureDate = "2023/02/08"
            });

            TestData.Add(new FlightDataModel
            {
                Id = 2,
                Airline = "TestAirline2",
                From = "X",
                To = "Y",
                Price = 200,
                DepartureDate = "2023/02/10"
            });
        }
    }
}
