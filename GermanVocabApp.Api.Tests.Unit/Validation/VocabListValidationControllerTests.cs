using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using Moq;
using Osiris.FluentValidation.Testing;

namespace GermanVocabApp.Api.Tests.Integration.Validation;
public class VocabListValidationControllerTests
{
    private InlineValidator<int> _passingListValidator;
    private InlineValidator<int> _failingListValidator;

    private Mock<IValidator<ListRequest>> _mockListValidator;
    private Mock<IAggregateValidator<ItemRequest>> _mockAggregateValidator;


    private VocabListValidationController _validationController;
    private ListRequest _list;
    private ItemRequest _item;

    private ValidationResult _passResult;
    private ValidationResult _failResult;

    public VocabListValidationControllerTests()
    {
        _passingListValidator = StubFluentValidator.CreatePassing<int>();
        _passResult = _passingListValidator.Validate(It.IsAny<int>());

        _failingListValidator = StubFluentValidator.CreateFailing<int>();
        _failResult = _failingListValidator.Validate(It.IsAny<int>());

        _mockListValidator = new Mock<IValidator<ListRequest>>();
        _mockAggregateValidator = new Mock<IAggregateValidator<ItemRequest>>();

        var fixture = new Fixture();

        _list = fixture.Create<ListRequest>();
        _item = fixture.Create<ItemRequest>();

        _validationController = new(_mockListValidator.Object, _mockAggregateValidator.Object);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void Validate_ShouldCallItemValidatorCorrectNumberOfTimes(int listItemCount, int expectedCallCount)
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<ItemRequest>>()));

        var items = new ItemRequest[listItemCount];
        for (int i = 0; i < listItemCount; i++)
        {
            items[i] = _item;
        }
        _list.ListItems = items;

        ValidationResult testResult = _validationController.Validate(_list);

        _mockAggregateValidator.Verify(av => av.Validate(It.IsAny<IList<ItemRequest>>()), Times.Exactly(expectedCallCount));
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenListValid_AndNoListItems()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_passResult);

        _list.ListItems = Enumerable.Empty<ItemRequest>();

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.True(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndNoListItems()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_failResult);

        _list.ListItems = Enumerable.Empty<ItemRequest>();

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenListValid_AndListItemsValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<ItemRequest>>()))
            .Returns(Array.Empty<ValidationFailure>);

        _list.ListItems = new[] { _item, _item };

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.True(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListValid_AndListItemsNotValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<ItemRequest>>()))
            .Returns(new[] { _failResult.Errors[0] });

        _list.ListItems = new[] { _item };

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndListItemsValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_failResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<ItemRequest>>()))
            .Returns(Array.Empty<ValidationFailure>);

        _list.ListItems = new[] { _item };

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndListItemsNotValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<ListRequest>()))
            .Returns(_failResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<ItemRequest>>()))
            .Returns(new[] { _failResult.Errors[0] });

        _list.ListItems = new[] { _item };

        ValidationResult testResult = _validationController.Validate(_list);
        Assert.False(testResult.IsValid);
    }
}
