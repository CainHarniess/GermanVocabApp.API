using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListRequestToDto;

public class CreateListRequestToDtoConverterValueTests : ListRequestToDtoConverterSetup
{
    private readonly Mock<IConverter<ItemRequest[], VocabListItemDto[]>> _mockItemsConverter;
    private readonly CreateListRequestToDtoConverter _converter;

    public CreateListRequestToDtoConverterValueTests() : base()
    {
        _mockItemsConverter = new Mock<IConverter<ItemRequest[], VocabListItemDto[]>>();
        _converter = new CreateListRequestToDtoConverter(_mockItemsConverter.Object);
        _result = _converter.Convert(_request);
    }

    [Fact]
    public void Convert_ShouldSetIdToNull()
    {
        Assert.Null(_result.Id);
    }
}
