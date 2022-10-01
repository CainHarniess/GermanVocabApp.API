using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion;
public class ItemDtoErrorTests
{
    private VocabListItemDto _dto;

    public ItemDtoErrorTests()
    {
        var fixture = new Fixture();
        _dto = fixture.Create<VocabListItemDto>();
    }

    [Fact]
    public void ToResponnse_ShouldThrowError_WhenNoId()
    {
        _dto.Id = null;
        UnexpectedNullIdException e = Assert.Throws<UnexpectedNullIdException>(() => _dto.ToResponse());
        Assert.Contains("list item ID", e.Message);
    }

    [Fact]
    public void ToResponnse_ShouldThrowError_WhenNoListId()
    {
        _dto.VocabListId = null;
        UnexpectedNullIdException e = Assert.Throws<UnexpectedNullIdException>(() => _dto.ToResponse());
        Assert.Contains("list ID", e.Message);
    }

    [Fact]
    public void ToResponnse_ShouldNotThrowError_WhenIdsPresent()
    {
        Exception exception = Record.Exception(() => _dto.ToResponse());
        Assert.Null(exception);
    }
}