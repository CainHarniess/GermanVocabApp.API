using FluentValidation;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Data;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Validation;

public class WordValidatorFactoryTests
{
    private readonly WordValidatorFactory _factory;
    private readonly ItemRequest _item;

    public WordValidatorFactoryTests()
    {
        _factory = new WordValidatorFactory();
        _item = new ItemRequest();
    }

    [Theory]
    [InlineData(WordType.Noun, typeof(FluentNounValidator))]
    [InlineData(WordType.Verb, typeof(FluentVerbValidator))]
    [InlineData(WordType.Adjective, typeof(FluentModifierValidator))]
    [InlineData(WordType.Adverb, typeof(FluentModifierValidator))]
    public void Create_ShouldReturnValidatorOfCorrectType(WordType wordType, Type expectedType)
    {
        _item.WordType = wordType;
        IValidator<ItemRequest> result = _factory.Create(_item);
        Assert.IsType(expectedType, result);
    }

    [Fact]
    public void Create_ShouldThrowError_IfInvalidWordType()
    {
        _item.WordType = (WordType)99;
        Assert.Throws<ArgumentException>(() => _factory.Create(_item));
    }

    [Fact]
    public void Create_ShouldNotThrowError_IfValidWordType()
    {
        _item.WordType = It.IsAny<WordType>();
        Exception exception = Record.Exception(() => _factory.Create(_item));
        Assert.Null(exception);
    }
}
