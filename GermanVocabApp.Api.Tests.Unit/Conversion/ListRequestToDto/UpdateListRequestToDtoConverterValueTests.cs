using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.Tests.Unit.Conversion.ListRequestToDto;

public class UpdateListRequestToDtoConverterValueTests : ListRequestToDtoConverterSetup
{
    private readonly Mock<IChildResourceConverter<ItemRequest[], VocabListItemDto[]>> _mockItemsConverter;
    private readonly UpdateListRequestToDtoConverter _converter;

    private readonly Guid _testGuid;

    public UpdateListRequestToDtoConverterValueTests() : base()
    {
        _mockItemsConverter = new Mock<IChildResourceConverter<ItemRequest[], VocabListItemDto[]>>();
        _converter = new UpdateListRequestToDtoConverter(_mockItemsConverter.Object);

        _testGuid = _fixture.Create<Guid>();

    }

    [Fact]
    public void Convert_ShouldSetIdToNull()
    {
        var result = _converter.Convert(_request, _testGuid);
        Assert.Equal(_testGuid, result.Id);
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        var result = _converter.Convert(_request, _testGuid);
        Assert.Equal(result.Name, _request.Name);
        Assert.Equal(result.Description, _request.Description);
    }
}
