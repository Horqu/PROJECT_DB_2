using Moq;
using WebApi.Services;
using WebApi.Models;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

public class OcenyControllerTests
{
    [Fact]
    public void Get_ReturnsOkResult_WithAListOfOceny()
    {
        // Arrange
        var mockService = new Mock<IOcenaService>();
        mockService.Setup(service => service.GetOceny())
            .Returns(new List<Ocena>() { new Ocena(), new Ocena() });
        var controller = new OcenyController(mockService.Object);

        // Act
        var result = controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<Ocena>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
}
