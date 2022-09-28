using FluentValidation;
using FluentValidation.Results;
using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Api.VocabLists.Validation;
using GermanVocabApp.Core.Contracts;
using Moq;
using Osiris.FluentValidation.Testing;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit;
public class VocabListValidationControllerTests
{
    private VocabListValidationController<IListItemRequest> _validationController;
    private Mock<IValidator<IListRequest<IListItemRequest>>> _mockListValidator;
    private Mock<IFactory<FluentWordValidator, IListItemRequest>> _mockWordValidatorFactory;
    private Mock<IValidator<IListItemRequest>> _mockItemValidator;
    private Mock<IListRequest<IListItemRequest>> _mockList;
    private Mock<IListItemRequest> _mockItem;

    private InlineValidator<int> _passingListValidator;
    private InlineValidator<int> _failingListValidator;

    private ValidationResult _passResult;
    private ValidationResult _failResult;

    public VocabListValidationControllerTests()
    {
        _passingListValidator = StubFluentValidator.CreatePassing<int>();
        _failingListValidator = StubFluentValidator.CreateFailing<int>();
        _mockListValidator = new Mock<IValidator<IListRequest<IListItemRequest>>>();

        _mockItemValidator = new Mock<IValidator<IListItemRequest>>();
        _mockWordValidatorFactory = new Mock<IFactory<FluentWordValidator, IListItemRequest>>();
        //_mockWordValidatorFactory.Setup(f => f.Create(It.IsAny<IListItemRequest>()))
        //                         .Returns((FluentWordValidator)_mockItemValidator.Object);

        _validationController = new(_mockListValidator.Object, _mockWordValidatorFactory.Object);
        
        _mockList = new Mock<IListRequest<IListItemRequest>>();
        _mockItem = new Mock<IListItemRequest>();

        _passingListValidator = StubFluentValidator.CreatePassing<int>();
        _passResult = _passingListValidator.Validate(It.IsAny<int>());
        _failResult = _failingListValidator.Validate(It.IsAny<int>());
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

    //[Fact]
    //public void Validate_ShouldReturnValidResult_WhenListValid_AndListItemsValid()
    //{
    //    _mockListValidator.Setup(lv => lv.Validate(It.IsAny<IListRequest<IListItemRequest>>()))
    //        .Returns(_passResult);
    //    _mockItemValidator.Setup(iv => iv.Validate(It.IsAny<IListItemRequest>()))
    //        .Returns(_passResult);

    //    _mockList.Setup(l => l.ListItems).Returns(new[]{ _mockItem.Object, _mockItem.Object });

    //    ValidationResult testResult = _validationController.Validate(_mockList.Object);
    //    Assert.True(testResult.IsValid);
    //}
}
