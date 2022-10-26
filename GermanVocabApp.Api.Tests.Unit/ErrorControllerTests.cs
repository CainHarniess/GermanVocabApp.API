using GermanVocabApp.Api.Testing;
using Microsoft.AspNetCore.Mvc;

namespace GermanVocabApp.Api.Tests.Unit;

public class ErrorControllerTests
{
    private readonly ErrorController _controller;

    public ErrorControllerTests()
    {
        _controller = new ErrorController();
    }

    [Fact]
    public void ReturnNoContent_ShouldReturnNoContent()
    {
        IActionResult result = _controller.ReturnNoContent();
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void ThrowUnauthorised_ShouldReturnUnauthorised()
    {
        IActionResult result = _controller.ThrowUnauthorised();
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void ThrowUnauthorised_ShouldReturnBadRequest()
    {
        IActionResult result = _controller.ThrowBadRequest();
        Assert.IsType<BadRequestResult>(result);
    }
}
