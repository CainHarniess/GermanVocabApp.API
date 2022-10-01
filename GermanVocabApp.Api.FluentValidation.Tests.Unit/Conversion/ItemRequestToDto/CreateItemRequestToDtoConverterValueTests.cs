﻿using GermanVocabApp.Api.VocabLists.Conversion.Items;
using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ItemRequestToDto;

public class CreateItemRequestToDtoConverterValueTests : ItemRequestConverterValueTests
{
    private CreateItemRequestToDtoConverter _converter;
    public CreateItemRequestToDtoConverterValueTests() : base()
    {
        _converter = new CreateItemRequestToDtoConverter();
        _dto = _converter.Convert(_request);
    }

    [Fact]
    public void Convert_ShouldSetIdToNull()
    {
        Assert.Null(_dto.Id);
    }

    [Fact]
    public void Convert_ShouldSetListIdToNull()
    {
        Assert.Null(_dto.VocabListId);
    }

    [Fact]
    public void Convert_ShouldThrowException_WhenDtoIdNull()
    {
        _request.Id = null;
        Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_request));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenDtoIdNotNull()
    {
        Exception e = Record.Exception(() => _converter.Convert(_request));
        Assert.Null(e);
    }
}