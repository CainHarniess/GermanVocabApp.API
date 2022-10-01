using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion;

public class ItemRequestUpdateValueTests : ItemRequestValueTests
{
    private Guid _testGuid;
    public ItemRequestUpdateValueTests() : base()
    {
        _testGuid = Fixture.Create<Guid>();
        Dto = Request.ToUpdateDto(_testGuid);
    }

    [Fact]
    public void ToUpdateDto_ShouldCopyIdValue()
    {
        Assert.Equal(Request.Id, Dto.Id);
    }

    [Fact]
    public void ToUpdateDto_ShouldSetListIdFromArgument()
    {
        Assert.Equal(_testGuid, Dto.VocabListId);
    }
}
