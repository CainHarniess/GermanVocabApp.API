﻿using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class ItemDtoToResponseConverterErrorTests : ItemDtoToResponseConverterTestSetUp
{
    [Fact]
    public void ToResponse_ShouldThrowError_WhenNoId()
    {
        _dto.Id = null;
        UnexpectedNullIdException e = Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_dto));
        Assert.Contains("list item ID", e.Message);
    }

    [Fact]
    public void ToResponse_ShouldThrowError_WhenNoListId()
    {
        _dto.VocabListId = null;
        UnexpectedNullIdException e = Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_dto));
        Assert.Contains("list ID", e.Message);
    }

    [Fact]
    public void ToResponse_ShouldNotThrowError_WhenIdsPresent()
    {
        Exception exception = Record.Exception(() => _converter.Convert(_dto));
        Assert.Null(exception);
    }
}