using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListRequestToDto;

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

        _result = _converter.Convert(_request, _testGuid);
    }

    [Fact]
    public void Convert_ShouldSetIdToNull()
    {
        Assert.Equal(_testGuid, _result.Id);
    }
}
