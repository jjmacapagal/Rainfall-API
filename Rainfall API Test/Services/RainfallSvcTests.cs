using Moq;
using Rainfall_API.Exceptions.Rainfall;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Services;

namespace Rainfall_API_Test.Services
{
    [TestClass]
    public class RainfallSvcTests
    {
        [TestMethod]
        public async Task GetReadings_UnsuccessfulResponse_ThrowsException()
        {
            // Arrange
            var moqRepo = new Mock<IRainfallRepository>();
            moqRepo
                .Setup(wrapper => wrapper.GetRainfallByStationId(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new InvalidStationIdException());
            var rainfallSvc = new RainfallSvc(moqRepo.Object);
            // Act
            var exception = await Assert.ThrowsExceptionAsync<InvalidStationIdException>(async () =>
            {
                // Call the method that you expect to throw the exception
                await rainfallSvc.GetReadings("stationId", 0);
            });
            // Assert
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task GetReadings_SuccessfulResponse_ReturnsReadings()
        {
            // Arrange
            var expectedDateMeasured = DateTime.Parse("2024-04-06T12:00:00");
            var expectedAmoundMeasured = 10.5;
            var moqRepo = new Mock<IRainfallRepository>();
            moqRepo
                .Setup(wrapper => wrapper.GetRainfallByStationId(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<RainfallReading>{ new(expectedDateMeasured, expectedAmoundMeasured) });
            var rainfallSvc = new RainfallSvc(moqRepo.Object);
            // Act
            var actualReadings = await rainfallSvc.GetReadings("stationId", 0);
            // Assert
            Assert.IsNotNull(actualReadings);
            Assert.IsTrue(actualReadings.Count == 1); // Assuming only one reading is returned in the mocked response
            var result = actualReadings[0];
            Assert.AreEqual(expectedDateMeasured, result.DateMeasured);
            Assert.AreEqual(expectedAmoundMeasured, result.AmountMeasured);
        }
    }
}