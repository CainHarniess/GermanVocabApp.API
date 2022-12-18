using GermanVocabApp.DataAccess.EntityFramework.Vocab;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit;

public class ListRepositoryAddCommandTests : ListRepositoryTestConfiguration
{
    [Fact]
    public async void Add_ShouldAddListAndListItems()
    {
        VocabListItemDto[] newItems = new VocabListItemDto[]
        {
            ItemDtoBuilder.Spicy().AsNew().Build(),
            ItemDtoBuilder.ToChop().AsNew().Build(),
        };

        VocabListDto newListDto = ListDtoBuilder.Empty()
                                                 .AsNew()
                                                 .WithName(Guid.NewGuid().ToString())
                                                 .WithListItems(newItems)
                                                 .Build();

        using (GermanAppAppDbContext context = ContextOptions.BuildNewInMemoryContext())
        {
            VocabListRepositoryAsync repository = new(context);
            VocabListDto _ = await repository.Add(newListDto);
        }

        VocabList? listEntity = GetSingleOrDefaultIncludeItemsWhere(l => l.Name == newListDto.Name
                                                        && l.Description == newListDto.Description
                                                        && l.DeletedDate == null);

        Assert.NotNull(listEntity);
        Assert.True(listEntity!.CreatedDate >= TestStartTimeStamp);

        VocabListItem[] listItems = listEntity!.ListItems.ToArray();

        Assert.Equal(newItems.Length, listItems.Length);

        for (int i = 0; i < listItems.Length; i++)
        {
            Assert.True(listItems[i].CreatedDate >= TestStartTimeStamp);
        }
    }
}
