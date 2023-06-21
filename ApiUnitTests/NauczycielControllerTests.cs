using Moq;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using WebApi.Controllers;

public class NauczycielControllerTests
{
    [Fact]
    public void Get_ReturnsOkResult_WithADictionaryOfResults()
    {
        // Arrange
        int nauczycielId = 1;
        var mockService = new Mock<INauczycielService>();
        var expectedResult = new Dictionary<string, string>
        {
            {"nauczycielId1", "4.0"}
        };

        mockService.Setup(service => service.GetNauczycielIdAndSredniaOcen(nauczycielId))
            .Returns(expectedResult);

        var controller = new NauczycielController(mockService.Object);

        // Act
        var result = controller.Get(nauczycielId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<Dictionary<string, string>>(okResult.Value);
        Assert.Equal(expectedResult, returnValue);
    }
}
