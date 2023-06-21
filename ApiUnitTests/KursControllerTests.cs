using Moq;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;

public class KursControllerTests
{
    [Fact]
    public void GetCzestoscPiatki_ReturnsOkResult_WithADictionaryOfResults()
    {
        // Arrange
        int kursId = 1;
        var mockService = new Mock<IKursService>();
        var expectedResult = new Dictionary<string, string>
        {
            {"nazwaKursu1", "1"}
        };

        mockService.Setup(service => service.GetCzestoscPiatkiForKursId(kursId))
            .Returns(expectedResult);

        var controller = new KursController(mockService.Object);

        // Act
        var result = controller.GetCzestoscPiatki(kursId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Dictionary<string, string>>(okResult.Value);
        Assert.Equal(expectedResult, returnValue);
    }
}
