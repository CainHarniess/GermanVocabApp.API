using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
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
    public async Task<IActionResult> AddVocabList(CreateVocabListRequest request)
    {
        CreateVocabListDto dto = request.ToDto();
        Guid newListId = await _repository.Add(dto);
        return CreatedAtRoute(VocabListsRoutes.Root, newListId);
    }

    [HttpGet]
    public async Task<IActionResult> GetInfos()
    {
        IEnumerable<VocabListInfoDto> vocabLists = await _repository.GetVocabListInfos();
        IEnumerable<VocabListInfoResponse> responses = vocabLists.ToResponses();
        return Ok(responses);
    }

    [HttpGet]
    [Route("{id:guid}", Name = VocabListsRoutes.GetById)]
    public async Task<IActionResult> Get(Guid id)
    {
        VocabListDto? dto = await _repository.Get(id);

        if (dto == null)
        {
            return NotFound();
        }

        VocabListResponse response = dto.ToResponse();
        return Ok(response);
    }

    //[HttpPut]
    //[Route("{id:guid}")]
    //public async Task<IActionResult> Update(Guid id, VocabListCreationDto vocabListRequest)
    //{
    //    VocabList vocabList = vocabListRequest.ToDomainModel();
    //    vocabList.Id = id;

    //    try
    //    {
    //        await _repository.Edit(vocabList);
    //        return NoContent();
    //    }
    //    catch (KeyNotFoundException e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    private struct VocabListsRoutes
    {
        public const string Root = "VocabLists";
        public const string GetById = "VocabLists.GetById";
    }
}
