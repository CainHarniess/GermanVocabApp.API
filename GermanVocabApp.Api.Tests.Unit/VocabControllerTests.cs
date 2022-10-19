using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GermanVocabApp.Api.Tests.Unit;
public class VocabControllerTests
{
    private readonly Fixture _fixture;

    private readonly Mock<IVocabListRepositoryAsync> _mockRepository;
    private readonly Mock<IValidationController<ListRequest>> _mockValidator;
    private readonly Mock<IConverter<VocabListDto, ListResponse>> _mockResponseConverter;
    private readonly Mock<IConverter<VocabListInfoDto[], ListInfoResponse[]>> _mockInfoResponseConverter;
    private readonly Mock<IConverter<ListRequest, VocabListDto>> _mockCreateRequestConverter;
    private readonly Mock<IUpdateResourceConverter<ListRequest, VocabListDto>> _mockUpdateRequestConverter;
    private readonly VocabListsController _controller;

    public VocabControllerTests()
    {
        _fixture = new();
        _mockRepository = new Mock<IVocabListRepositoryAsync>();
        _mockValidator = new Mock<IValidationController<ListRequest>>();
        _mockResponseConverter = new Mock<IConverter<VocabListDto, ListResponse>>();
        _mockInfoResponseConverter = new Mock<IConverter<VocabListInfoDto[], ListInfoResponse[]>>();
        _mockCreateRequestConverter = new Mock<IConverter<ListRequest, VocabListDto>>();
        _mockUpdateRequestConverter = new Mock<IUpdateResourceConverter<ListRequest, VocabListDto>>();

        _controller = new VocabListsController(_mockValidator.Object, _mockRepository.Object, _mockResponseConverter.Object,
                                               _mockInfoResponseConverter.Object, _mockCreateRequestConverter.Object,
                                               _mockUpdateRequestConverter.Object);
    }

    [Fact]
    public async void Get_ShouldReturnNotFound_IfRepositoryReturnsNull()
    {
        Guid testId = Guid.NewGuid();
        _ = _mockRepository.Setup(r => r.Get(testId)).ReturnsAsync((VocabListDto)null);

        IActionResult result = await _controller.Get(testId);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Create_ShouldReturnBadRequest_IfValidationFails()
    {
        ListRequest request = ConfigureInvalidRequest();
        IActionResult result = await _controller.Create(request);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Create_ShouldThrowInternalServerError_IfRepositoryCreatesBadDto()
    {
        ListRequest request = _fixture.Create<ListRequest>();
        _mockValidator.Setup(r => r.Validate(request)).Returns(() => new ValidationResult()
        {
            Errors = new List<ValidationFailure>(0)
        });
        VocabListDto convertResult = _fixture.Create<VocabListDto>();
        _mockCreateRequestConverter.Setup(c => c.Convert(It.IsAny<ListRequest>())).Returns(convertResult);

        VocabListDto addResult = _fixture.Create<VocabListDto>();
        addResult.Id = null;
        _mockRepository.Setup(r => r.Add(It.IsAny<VocabListDto>())).ReturnsAsync(addResult);

        IActionResult result = await _controller.Create(request);

        Assert.IsType<ObjectResult>(result);
        ObjectResult objectResult = (ObjectResult)result;
        Assert.Equal(objectResult.StatusCode, StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async void Create_ShouldReturnCreated_IfRequestIsValid()
    {
        ListRequest request = ConfigureValidRequest();
        VocabListDto convertResult = _fixture.Create<VocabListDto>();
        _mockCreateRequestConverter.Setup(c => c.Convert(It.IsAny<ListRequest>())).Returns(convertResult);

        VocabListDto addResult = _fixture.Create<VocabListDto>();
        _mockRepository.Setup(r => r.Add(It.IsAny<VocabListDto>())).ReturnsAsync(addResult);

        ListResponse responseConvertResult = _fixture.Create<ListResponse>();
        _mockResponseConverter.Setup(c => c.Convert(It.IsAny<VocabListDto>())).Returns(responseConvertResult);

        IActionResult result = await _controller.Create(request);

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async void HardDelete_ShouldReturnNotFound_IfRepositoryReturnsFalse()
    {
        _mockRepository.Setup(r => r.HardDelete(It.IsAny<Guid>())).ReturnsAsync(false);
        IActionResult result = await _controller.HardDelete(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void HardDelete_ShouldReturnNotFound_IfRepositoryReturnsTrue()
    {
        _mockRepository.Setup(r => r.HardDelete(It.IsAny<Guid>())).ReturnsAsync(true);
        IActionResult result = await _controller.HardDelete(Guid.NewGuid());
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void Update_ShouldReturnBadRequest_IfRequestInvalid()
    {
        ListRequest request = ConfigureInvalidRequest();
        IActionResult result = await _controller.UpdateVocabList(Guid.NewGuid(), request);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async void Update_ShouldReturnNotFound_IfRepositoryThrowsEntityNotFoundException()
    {
        ListRequest request = ConfigureValidRequest();
        VocabListDto dto = ConfigureUpdateConverter();

        EntityNotFoundException e = new();
        _mockRepository.Setup(r => r.Update(It.IsAny<VocabListDto>())).ThrowsAsync(e);

        IActionResult result = await _controller.UpdateVocabList(Guid.NewGuid(), request);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async void Update_ShouldReturnInternalServerError_IfRepositoryThrowsUnexpectedIdException()
    {
        ListRequest request = ConfigureValidRequest();
        VocabListDto dto = ConfigureUpdateConverter();

        UnexpectedIdException e = new();
        _mockRepository.Setup(r => r.Update(It.IsAny<VocabListDto>())).ThrowsAsync(e);

        IActionResult result = await _controller.UpdateVocabList(Guid.NewGuid(), request);
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(((ObjectResult)result).StatusCode, StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public async void Update_ShouldReturnNoContent_IfRequestIsValid()
    {
        ListRequest request = ConfigureValidRequest();
        VocabListDto dto = ConfigureUpdateConverter();

        IActionResult result = await _controller.UpdateVocabList(Guid.NewGuid(), request);
        Assert.IsType<NoContentResult>(result);
    }

    private VocabListDto ConfigureUpdateConverter()
    {
        VocabListDto dto = _fixture.Create<VocabListDto>();
        _mockUpdateRequestConverter.Setup(c => c.Convert(It.IsAny<ListRequest>(), It.IsAny<Guid>()))
                                   .Returns(dto);
        return dto;
    }

    private ListRequest ConfigureValidRequest()
    {
        ListRequest request = _fixture.Create<ListRequest>();
        _mockValidator.Setup(r => r.Validate(request)).Returns(() => new ValidationResult()
        {
            Errors = new List<ValidationFailure>(0)
        });
        return request;
    }

    private ListRequest ConfigureInvalidRequest()
    {
        ListRequest request = _fixture.Create<ListRequest>();
        _mockValidator.Setup(r => r.Validate(request)).Returns(() => new ValidationResult()
        {
            Errors = new List<ValidationFailure>() { new ValidationFailure("TestProperty", "Something went wrong."), }
        });
        return request;
    }
}
