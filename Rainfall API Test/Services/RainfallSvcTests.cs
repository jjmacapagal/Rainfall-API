using Moq;
using Rainfall_API.Exceptions.Rainfall;
using Rainfall_API.Interfaces;
using Rainfall_API.Services;
using System.Net;

namespace Rainfall_API_Test.Services
{
    [TestClass]
    public class RainfallSvcTests
    {
        [TestMethod]
        public async Task GetReadings_UnsuccessfulResponse_ThrowsException()
        {
            // Arrange
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            httpClientWrapperMock.Setup(wrapper => wrapper.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var rainfallSvc = new RainfallSvc(httpClientWrapperMock.Object);

            var exception = await Assert.ThrowsExceptionAsync<InvalidStationIdException>(async () =>
            {
                // Call the method that you expect to throw the exception
                await rainfallSvc.GetReadings("stationId");
            });

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task GetReadings_SuccessfulResponse_ReturnsReadings()
        {
            // Arrange
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            httpClientWrapperMock.Setup(wrapper => wrapper.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"items\":[{\"dateTime\":\"2024-04-06T12:00:00\",\"value\":10.5}]}")
                });

            var rainfallSvc = new RainfallSvc(httpClientWrapperMock.Object);

            // Act
            var actualReadings = await rainfallSvc.GetReadings("stationId");

            // Assert
            Assert.IsNotNull(actualReadings);
            Assert.IsTrue(actualReadings.Count == 1); // Assuming only one reading is returned in the mocked response
            var result = actualReadings[0];
            Assert.AreEqual(new DateTime(2024, 04, 06, 12, 00, 00), result.DateMeasured);
            Assert.AreEqual(10.5, result.AmountMeasured);
        }
    }
}