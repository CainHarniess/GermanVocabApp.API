using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Items;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class UpdateItemRequestToDtoConverterTests : ItemRequestConverterTests
{
    private UpdateItemRequestToDtoConverter _converter;
    private Guid _testGuid;
    public UpdateItemRequestToDtoConverterTests() : base()
    {
        _converter = new UpdateItemRequestToDtoConverter();
        _testGuid = _fixture.Create<Guid>();
        _dto = _converter.Convert(_request, _testGuid);
    }

    [Fact]
    public void Convert_ShouldCopyIdValue()
    {
        Assert.Equal(_request.Id, _dto.Id);
    }

    [Fact]
    public void Convert_ShouldSetListIdFromArgument()
    {
        Assert.Equal(_testGuid, _dto.VocabListId);
    }
}
