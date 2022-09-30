using FluentValidation;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Shared.Data;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit;

public class WordValidatorFactoryTests
{
    private readonly WordValidatorFactory _factory;

    public WordValidatorFactoryTests()
    {
        _factory = new WordValidatorFactory();
    }

    [Theory]
    [InlineData(WordType.Noun, typeof(FluentNounValidator))]
    [InlineData(WordType.Verb, typeof(FluentVerbValidator))]
    [InlineData(WordType.Adjective, typeof(FluentModifierValidator))]
    [InlineData(WordType.Adverb, typeof(FluentModifierValidator))]
    public void Create_ShouldReturnValidatorOfCorrectType(WordType wordType, Type expectedType)
    {
        Mock<IListItemRequest> mockListItem = new Mock<IListItemRequest>();
        mockListItem.Setup(li => li.WordType).Returns(wordType);
        IValidator<IListItemRequest> result = _factory.Create(mockListItem.Object);
        Assert.IsType(expectedType, result);
    }

    [Fact]
    public void Create_ShouldThrowError_IfInvalidWordType()
    {
        Mock<IListItemRequest> mockListItem = new Mock<IListItemRequest>();
        mockListItem.Setup(li => li.WordType).Returns((WordType)99);
        Assert.Throws<ArgumentException>(() => _factory.Create(mockListItem.Object));
    }

    [Fact]
    public void Create_ShouldNotThrowError_IfValidWordType()
    {
        Mock<IListItemRequest> mockListItem = new Mock<IListItemRequest>();
        mockListItem.Setup(li => li.WordType).Returns(It.IsAny<WordType>());
        Exception exception = Record.Exception(() => _factory.Create(mockListItem.Object));
        Assert.Null(exception);
    }
}
