using GermanVocabApp.Api.VocabLists.Conversion;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion;

public class ItemRequestCreationValueTests : ItemRequestValueTests
{
    public ItemRequestCreationValueTests() : base()
    {
        Dto = Request.ToCreationDto();
    }

    [Fact]
    public void ToCreationDto_ShouldSetIdToNull()
    {
        Assert.Null(Dto.Id);
    }

    [Fact]
    public void ToCreationDto_ShouldSetListIdToNull()
    {
        Assert.Null(Dto.VocabListId);
    }
}
