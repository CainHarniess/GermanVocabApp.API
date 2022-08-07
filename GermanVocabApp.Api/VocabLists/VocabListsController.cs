using AutoMapper;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Domain.Abstractions;
using GermanVocabApp.Domain.VocabListAggregate;
using Microsoft.AspNetCore.Mvc;

namespace GermanVocabApp.Api.VocabLists;

[ApiController]
[Route("api/vocab-lists", Name = VocabListsRoutes.Root)]
public class VocabListsController : ControllerBase
{
    private readonly IVocabListRepositoryAsync _repository;
    private readonly IMapper _mapper;

    public VocabListsController(IVocabListRepositoryAsync repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddVocabList(VocabListCreationDto vocabListRequest)
    {
        VocabList vocabList= _mapper.Map<VocabList>(vocabListRequest);
        await _repository.Add(vocabList);
        return CreatedAtRoute(VocabListsRoutes.Root, vocabList.Id);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<VocabList> vocabLists = await _repository.GetAll();
        IEnumerable<VocabListInfoDto> infoDtos = _mapper.Map<IEnumerable<VocabListInfoDto>>(vocabLists);
        return Ok(infoDtos);
    }

    [HttpGet]
    [Route("{id:guid}", Name = VocabListsRoutes.Add)]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            VocabList? vocabListInfo = await _repository.Get(id);
            VocabListInfoDto listInfoDto = _mapper.Map<VocabListInfoDto>(vocabListInfo);
            return Ok(listInfoDto);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, VocabListCreationDto vocabListRequest)
    {
        VocabList vocabList = vocabListRequest.ToDomainModel();
        vocabList.Id = id;

        try
        {
            await _repository.Edit(vocabList);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    private struct VocabListsRoutes
    {
        public const string Root = "Vocab Lists";
        public const string Add = "VocabList.Add";
    }
}
