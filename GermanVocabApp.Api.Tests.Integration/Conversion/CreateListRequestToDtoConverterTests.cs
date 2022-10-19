using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.Tests.Integration.Conversion;

public class CreateListRequestToDtoConverterTests
{
    private Fixture _fixture;
    private ListRequest _request;
    private Mock<IConverter<ItemRequest[], VocabListItemDto[]>> _mockItemConverter;
    private CreateListRequestToDtoConverter _converter;

    public CreateListRequestToDtoConverterTests()
    {
        _fixture = new Fixture();
        _request = _fixture.Create<ListRequest>();

        _mockItemConverter = new Mock<IConverter<ItemRequest[], VocabListItemDto[]>>();
        _converter = new(_mockItemConverter.Object);
    }

    [Fact]
    public void Convert_ShouldCallItemConverter_WithCorrectValue()
    {
        _mockItemConverter.Setup(ic => ic.Convert(_request.ListItems.ToArray()));
        _request.Id = null;
        _converter.Convert(_request);
        _mockItemConverter.Verify(m => m.Convert(_request.ListItems.ToArray()), Times.Once);
    }
}