using holiday.search.Controllers;

namespace holiday.search.tests
{
    public class UnitTest1
    {
        private readonly TestController _sut;

        public UnitTest1()
        {
            _sut = new TestController();
        }

        [Fact]
        public void Test1()
        {
            // Act
            var result = _sut.Get();

            //Assert
            Assert.True(result);
        }
    }
}