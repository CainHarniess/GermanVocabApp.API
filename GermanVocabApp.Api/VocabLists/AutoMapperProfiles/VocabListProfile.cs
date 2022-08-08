using AutoMapper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Domain.VocabListAggregate;

namespace GermanVocabApp.Api.VocabLists.AutoMapperProfiles;

internal class VocabListProfile : Profile
{
    public VocabListProfile()
    {
        CreateMap<VocabListCreationDto, VocabList>();
        CreateMap<VocabList, VocabListInfoResponse>();
        CreateMap<VocabList, FullVocabListDto>();
    }
}
