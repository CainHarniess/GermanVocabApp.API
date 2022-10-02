using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListDtoToResponse;

public abstract class ListDtoToResponseConverterTests
{
    protected Fixture _fixture;
    protected VocabListDto _dto;
    protected Mock<IConverter<VocabListItemDto[], ItemResponse[]>> _mockItemConverter;
    protected ListDtoToResponseConverter _converter;

    protected ListDtoToResponseConverterTests()
    {
        _fixture = new Fixture();
        _dto = _fixture.Create<VocabListDto>();

        _mockItemConverter = new Mock<IConverter<VocabListItemDto[], ItemResponse[]>>();
        _converter = new(_mockItemConverter.Object);
    }
}
