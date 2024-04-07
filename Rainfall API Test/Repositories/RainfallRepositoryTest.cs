using Moq;
using Rainfall_API.Exceptions.Rainfall;
using Rainfall_API.Interfaces;
using Rainfall_API.Models.API;
using Rainfall_API.Repositories;
using System.Net;

namespace Rainfall_API_Test.Repositories
{
    [TestClass]
    public class RainfallRepositoryTest
    {
        [TestMethod]
        public async Task GetRainfallByStationId_UnsuccessfulResponse_ThrowsException()
        {
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            httpClientWrapperMock.Setup(wrapper => wrapper.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var repo = new RainfallRepository(httpClientWrapperMock.Object);

            var exception = await Assert.ThrowsExceptionAsync<InvalidStationIdException>(async () =>
            {
                // Call the method that you expect to throw the exception
                await repo.GetRainfallByStationId("stationId", 0);
            });

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task GetRainfallByStationId_SuccessfulResponse_ReturnsReadings()
        {
            // Arrange
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var expectedDateMeasured = DateTime.Parse("2024-04-06T12:00:00");
            var expectedDateMeasuredString = expectedDateMeasured.ToString("yyyy-MM-ddTHH:mm:ss");
            var expectedAmoundMeasured = 10.5;
            var content = new StringContent($"{{\"items\":[{{\"dateTime\":\"{expectedDateMeasuredString}\",\"value\":{expectedAmoundMeasured}}}]}}");
            httpClientWrapperMock
                .Setup(wrapper => wrapper.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = content
                });

            var repo = new RainfallRepository(httpClientWrapperMock.Object);

            // Act
            var result = await repo.GetRainfallByStationId("stationId", 0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1); // Assuming only one reading is returned in the mocked response
            var reading = result[0];
            Assert.AreEqual(expectedDateMeasured, reading.DateMeasured);
            Assert.AreEqual(expectedAmoundMeasured, reading.AmountMeasured);
        }
    }
}