using Moq;
using Rainfall_API.Exceptions.Rainfall;
using Rainfall_API.Handlers;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Queries;

namespace Rainfall_API_Test.Handlers
{
    [TestClass]
    public class GetRainfallReadingsHandlerTest
    {
        [TestMethod]
        public async Task Handle_UnsuccessfulResponse_ThrowsException()
        {
            // Arrange
            var mockService = new Mock<IRainfallSvc>();
            mockService
                .Setup(x => x.GetReadings(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new InvalidStationIdException());
            var query = new GetRainfallReadingsQuery("");
            var handler = new GetRainfallReadingsHandler(mockService.Object);
            // Act
            var exception = await Assert.ThrowsExceptionAsync<InvalidStationIdException>(async () =>
            {
                // Call the method that you expect to throw the exception
                await handler.Handle(query, new CancellationToken());
            });
            // Assert
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task Handle_SuccessfulResponse_ReturnsReadings()
        {                        
            // Arrange
            var expectedDateMeasured = DateTime.Parse("2024-04-06T12:00:00");
            var expectedAmoundMeasured = 10.5;
            var mockService = new Mock<IRainfallSvc>();
            mockService
                .Setup(x => x.GetReadings(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<RainfallReading>{ new(expectedDateMeasured, expectedAmoundMeasured) });
            var query = new GetRainfallReadingsQuery("");
            var handler = new GetRainfallReadingsHandler(mockService.Object);
            // Act
            var actualReadings = await handler.Handle(query, new CancellationToken());
            // Assert
            Assert.IsNotNull(actualReadings);
            Assert.IsTrue(actualReadings.Count == 1); // Assuming only one reading is returned in the mocked response
            var result = actualReadings[0];
            Assert.AreEqual(expectedDateMeasured, result.DateMeasured);
            Assert.AreEqual(expectedAmoundMeasured, result.AmountMeasured);
        }
    }
}