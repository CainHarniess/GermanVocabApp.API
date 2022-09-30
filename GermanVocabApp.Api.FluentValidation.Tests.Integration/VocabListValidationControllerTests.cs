using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using Moq;
using Osiris.FluentValidation.Testing;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit;
public class VocabListValidationControllerTests
{
    private InlineValidator<int> _passingListValidator;
    private InlineValidator<int> _failingListValidator;
    
    private Mock<IValidator<IListRequest<IListItemRequest>>> _mockListValidator;
    private Mock<IAggregateValidator<IListItemRequest>> _mockAggregateValidator;


    private VocabListValidationController<IListItemRequest> _validationController;
    private Mock<IListRequest<IListItemRequest>> _mockList;
    private Mock<IListItemRequest> _mockItem;

    private ValidationResult _passResult;
    private ValidationResult _failResult;

    public VocabListValidationControllerTests()
    {
        _passingListValidator = StubFluentValidator.CreatePassing<int>();
        _passResult = _passingListValidator.Validate(It.IsAny<int>());
        
        _failingListValidator = StubFluentValidator.CreateFailing<int>();
        _failResult = _failingListValidator.Validate(It.IsAny<int>());

        _mockListValidator = new Mock<IValidator<IListRequest<IListItemRequest>>>();
        _mockAggregateValidator = new Mock<IAggregateValidator<IListItemRequest>>();

        _mockList = new Mock<IListRequest<IListItemRequest>>();
        _mockItem = new Mock<IListItemRequest>();

        _validationController = new(_mockListValidator.Object, _mockAggregateValidator.Object);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(5, 1)]
    public void Validate_ShouldCallItemValidatorCorrectNumberOfTimes(int listItemCount, int expectedCallCount)
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<IListItemRequest>>()));

        IListItemRequest[] items = new IListItemRequest[listItemCount];
        for (int i = 0; i < listItemCount; i++)
        {
            items[i] = _mockItem.Object;
        }
        _mockList.Setup(l => l.ListItems).Returns(items);

        ValidationResult testResult = _validationController.Validate(_mockList.Object);

        _mockAggregateValidator.Verify(av => av.Validate(It.IsAny<IList<IListItemRequest>>()), Times.Exactly(expectedCallCount));
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenListValid_AndNoListItems()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_passResult);

        _mockList.Setup(l => l.ListItems).Returns(Enumerable.Empty<IListItemRequest>());

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.True(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndNoListItems()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_failResult);

        _mockList.Setup(l => l.ListItems).Returns(Enumerable.Empty<IListItemRequest>());

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenListValid_AndListItemsValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<IListItemRequest>>()))
            .Returns(Array.Empty<ValidationFailure>);

        _mockList.Setup(l => l.ListItems).Returns(new[] { _mockItem.Object, _mockItem.Object });

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.True(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListValid_AndListItemsNotValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_passResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<IListItemRequest>>()))
            .Returns(new[] { _failResult.Errors[0] });

        _mockList.Setup(l => l.ListItems).Returns(new[] { _mockItem.Object });

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndListItemsValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_failResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<IListItemRequest>>()))
            .Returns(Array.Empty<ValidationFailure>);

        _mockList.Setup(l => l.ListItems).Returns(new[] { _mockItem.Object });

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.False(testResult.IsValid);
    }

    [Fact]
    public void Validate_ShouldReturnInvalidResult_WhenListNotValid_AndListItemsNotValid()
    {
        _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
            .Returns(_failResult);

        _mockAggregateValidator.Setup(av => av.Validate(It.IsAny<IList<IListItemRequest>>()))
            .Returns(new[] { _failResult.Errors[0] });

        _mockList.Setup(l => l.ListItems).Returns(new[] { _mockItem.Object });

        ValidationResult testResult = _validationController.Validate(_mockList.Object);
        Assert.False(testResult.IsValid);
    }
}
