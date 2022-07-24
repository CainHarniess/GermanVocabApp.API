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

    public VocabListsController(IVocabListRepositoryAsync repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> AddVocabList(VocabListRequestDto vocabListRequest)
    {
        VocabList vocabList = vocabListRequest.ToDomainModel();
        await _repository.Add(vocabList);
        return CreatedAtRoute(VocabListsRoutes.Root, vocabList.Id);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IEnumerable<VocabList> vocabLists = await _repository.GetAll();
        IEnumerable<VocabListResponseDto> responseObjects = vocabLists.ToResponseDtos();
        return Ok(responseObjects);
    }

    [HttpGet]
    [Route("{id:guid}", Name = VocabListsRoutes.Add)]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            VocabList? vocabList = await _repository.Get(id);
            return Ok(vocabList!.ToResponseDto());
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, VocabListRequestDto vocabListRequest)
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
