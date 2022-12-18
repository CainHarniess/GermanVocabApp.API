using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;
using Moq;

namespace GermanVocabApp.Api.Tests.Integration.Conversion;

public class AggregateConverterTests
{
    private Fixture _fixture;
    private IEnumerable<VocabListItemDto> _dtos;
    private Mock<IConverter<VocabListItemDto, ItemResponse>> _mockItemConverter;
    private AggregateConverter<VocabListItemDto, ItemResponse> _converter;

    public AggregateConverterTests()
    {
        _fixture = new Fixture();
        _dtos = _fixture.Create<IEnumerable<VocabListItemDto>>();

        _mockItemConverter = new Mock<IConverter<VocabListItemDto, ItemResponse>>();
        _converter = new(_mockItemConverter.Object);
    }

    [Fact]
    public void Convert_ShouldCallItemConverter_WithCorrectValue()
    {
        _mockItemConverter.Setup(c => c.Convert(It.IsAny<VocabListItemDto>()));
        var response = _converter.Convert(_dtos.ToArray());
        _mockItemConverter.Verify(c => c.Convert(It.IsAny<VocabListItemDto>()), Times.Exactly(_dtos.Count()));
    }
}