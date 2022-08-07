using AutoMapper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.AutoMapperProfiles;

internal class VocabListItemProfile : Profile
{
    public VocabListItemProfile()
    {
        CreateMap<VocabListItemCreationDto, VocabListItem>();
        CreateMap<VocabListItem, VocabListItemDto>();
    }
}
