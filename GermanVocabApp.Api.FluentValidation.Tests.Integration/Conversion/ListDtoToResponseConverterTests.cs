using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Integration.Conversion;
public class ListDtoToResponseConverterTests
{
    private Fixture _fixture;
    private VocabListDto _listDto;
    private Mock<IConverter<VocabListItemDto[], ItemResponse[]>> _mockItemConverter;
    private ListDtoToResponseConverter _converter;

    public ListDtoToResponseConverterTests()
    {
        _fixture = new Fixture();
        _listDto = _fixture.Create<VocabListDto>();

        _mockItemConverter = new Mock<IConverter<VocabListItemDto[], ItemResponse[]>>();
        _converter = new(_mockItemConverter.Object);
    }

    [Fact]
    public void Convert_ShouldCallItemConverter_WithCorrectValue()
    {
        _mockItemConverter.Setup(ic => ic.Convert(_listDto.ListItems.ToArray()));
        _converter.Convert(_listDto);
        _mockItemConverter.Verify(m => m.Convert(_listDto.ListItems.ToArray()), Times.Once);
    }
}
